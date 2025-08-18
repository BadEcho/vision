// -----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2025 Bad Echo LLC. All rights reserved.
//
//      Bad Echo Technologies are licensed under the
//      GNU Affero General Public License v3.0.
//
//      See accompanying file LICENSE.md or a copy at:
//      https://www.gnu.org/licenses/agpl-3.0.html
// </copyright>
// -----------------------------------------------------------------------

using System.Windows.Threading;
using BadEcho.Presentation.Windows;
using BadEcho.Configuration;
using BadEcho.Extensibility.Hosting;
using BadEcho.Vision.Extensibility;
using BadEcho.Vision.ViewModels;

namespace BadEcho.Vision.Windows;

/// <summary>
/// Provides an assembler of a Vision main window's data context.
/// </summary>
internal sealed class VisionContextAssembler : IContextAssembler<VisionViewModel>
{                   
    private readonly VisionViewModel _viewModel = new();
    
    private Dispatcher? _dispatcher;
    private bool _isAssembled;
        
    /// <inheritdoc/>
    public async Task<VisionViewModel> Assemble(Dispatcher dispatcher)
    {
        if (_isAssembled)
            return _viewModel;

        _dispatcher = dispatcher;

        IConfigurationProvider configurationProvider
            = PluginHost.LoadFromProcess<IConfigurationProvider>();

        configurationProvider.ConfigurationChanged 
            += async (s, _) => await HandleConfigurationChanged(s);

        await ApplyConfiguration(configurationProvider);

        _isAssembled = true;

        return _viewModel;
    }

    private async Task ApplyConfiguration(IConfigurationProvider configurationProvider)
    {
        VisionConfiguration configuration
            = configurationProvider.GetConfiguration<VisionConfiguration>();

        configuration.Dispatcher = _dispatcher;

        _viewModel.Disconnect();
        _viewModel.ApplyConfiguration(configuration);

        var titleModuleHost = ModuleHost.ForTitle(configuration);

        _viewModel.Bind(titleModuleHost);

        var modules
            = PluginHost.ArmedLoad<IVisionModule, IVisionConfiguration>(configuration);

        foreach (var module in modules)
        {
            var moduleHost = await ModuleHost.Create(module, configuration);
                
            _viewModel.Bind(moduleHost);
        }
    }

    private async Task HandleConfigurationChanged(object? sender)
    {
        if (sender is not IConfigurationProvider configurationProvider)
            return;

        await ApplyConfiguration(configurationProvider);
    }
}