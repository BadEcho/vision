//-----------------------------------------------------------------------
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

using System.Windows;
using BadEcho.Vision.Windows;

namespace BadEcho.Vision;

/// <summary>
/// Provides the Vision application.
/// </summary>
public partial class App
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App() 
        => Starting += async (_, e) => await HandleStarting(e);

    private event EventHandler<EventArgs<VisionWindow>> Starting;

    /// <inheritdoc/>
    protected override void OnStartup(StartupEventArgs e)
    {
        var window = new VisionWindow();

        Starting.Invoke(this, new EventArgs<VisionWindow>(window));

        base.OnStartup(e);
        
        window.Show();
    }

    private static async Task HandleStarting(EventArgs<VisionWindow> e)
    {
        var contextAssembler = new VisionContextAssembler();

        await e.Data.AssembleContext(contextAssembler);
    }
}