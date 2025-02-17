﻿//-----------------------------------------------------------------------
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
//-----------------------------------------------------------------------

using BadEcho.Presentation.ViewModels;
using BadEcho.Vision.Extensibility;

namespace BadEcho.Vision.ViewModels;

/// <summary>
/// Provides the root view model for the Vision application, which acts as a collection of hosted Vision modules.
/// </summary>
internal sealed class VisionViewModel : CollectionViewModel<ModuleHost, ModuleHostViewModel>
{
    private AnchorPointLocation _titleLocation;
        
    /// <summary>
    /// Initializes a new instance of the <see cref="VisionViewModel"/> class.
    /// </summary>
    public VisionViewModel()
        : base(new CollectionViewModelOptions())
    { }

    /// <summary>
    /// Gets or sets the location for the Vision application title's anchor point.
    /// </summary>
    public AnchorPointLocation TitleLocation
    {
        get => _titleLocation;
        set => NotifyIfChanged(ref _titleLocation, value);
    }

    /// <inheritdoc/>
    public override ModuleHostViewModel CreateItem(ModuleHost model)
    {
        var viewModel = new ModuleHostViewModel();
            
        viewModel.Bind(model);

        return viewModel;
    }
        
    /// <inheritdoc/>
    public override void UpdateItem(ModuleHost model)
    {
        var existingChild = FindItem<ModuleHostViewModel>(model);

        existingChild?.Bind(model);
    }

    /// <summary>
    /// Applies the provided Vision application configuration to this root view model instance.
    /// </summary>
    /// <param name="configuration">The Vision application configuration to apply to this view model.</param>
    public void ApplyConfiguration(IVisionConfiguration configuration)
    {
        Require.NotNull(configuration, nameof(configuration));

        TitleLocation = configuration.TitleLocation;
    }
}