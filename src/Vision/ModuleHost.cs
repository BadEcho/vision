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

using BadEcho.Presentation.ViewModels;
using BadEcho.Vision.Extensibility;
using BadEcho.Vision.ViewModels;

namespace BadEcho.Vision;

/// <summary>
/// Provides a host container for a Vision module.
/// </summary>
internal sealed class ModuleHost : IDisposable
{
    private readonly MessageFileWatcher? _messageFileWatcher;

    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleHost"/> class.
    /// </summary>
    /// <param name="module">The Vision module to be hosted.</param>
    /// <param name="messageFileWatcher">A file watcher that will feed the module with new messages being posted.</param>
    /// <param name="moduleViewModel">The view model to use to display the module.</param>
    private ModuleHost(IVisionModule module, MessageFileWatcher messageFileWatcher, IViewModel moduleViewModel)
        : this(module.Location, moduleViewModel)
    {
        _messageFileWatcher = messageFileWatcher;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleHost"/> class.
    /// </summary>
    /// <param name="location">An enumeration value specifying anchor point to attach the module to.</param>
    /// <param name="moduleViewModel">The view model to use to display the module.</param>
    private ModuleHost(AnchorPointLocation location, IViewModel moduleViewModel)
    {
        Location = location;
        ModuleViewModel = moduleViewModel;
    }

    /// <summary>
    /// Gets the view model exported by an activated Vision module.
    /// </summary>
    public IViewModel ModuleViewModel
    { get; }

    /// <summary>
    /// Gets the location of the anchor point for the Vision module.
    /// </summary>
    public AnchorPointLocation Location
    { get; }

    /// <summary>
    /// Creates a host container for the provided module.
    /// </summary>
    /// <param name="module">The Vision module to be hosted.</param>
    /// <param name="configuration">Configuration settings for the Vision application.</param>
    /// <returns>A <see cref="ModuleHost"/> instance for displaying <c>module</c>.</returns>
    public static async Task<ModuleHost> Create(IVisionModule module, IVisionConfiguration configuration)
    {
        Require.NotNull(module, nameof(module));
        Require.NotNull(configuration, nameof(configuration));

        var messageFileWatcher = new MessageFileWatcher(module, configuration.MessageFilesDirectory);
        IViewModel moduleViewModel = await module.EnableModule(messageFileWatcher);

        return new ModuleHost(module, messageFileWatcher, moduleViewModel);
    }

    /// <summary>
    /// Creates a host container for the module responsible for displaying the Vision application's title.
    /// </summary>
    /// <param name="configuration">Configuration settings for the Vision application.</param>
    /// <returns>A <see cref="ModuleHost"/> instance for displaying the Vision application's title.</returns>
    public static ModuleHost ForTitle(IVisionConfiguration configuration)
    {
        Require.NotNull(configuration, nameof(configuration));

        var titleViewModel = new VisionTitleViewModel();

        return new ModuleHost(configuration.TitleLocation, titleViewModel);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
            return;

        _messageFileWatcher?.Dispose();

        _disposed = true;
    }
}