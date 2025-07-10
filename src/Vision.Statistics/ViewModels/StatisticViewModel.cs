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

using BadEcho.Presentation.Messaging;
using BadEcho.Presentation.ViewModels;

namespace BadEcho.Vision.Statistics.ViewModels;

/// <summary>
/// Provides a base view model that facilitates the display of an individual statistic exported from an Omnified game.
/// </summary>
/// <typeparam name="TStatistic">The type of statistic bound to the view model for display on a view.</typeparam>
internal abstract class StatisticViewModel<TStatistic> : ViewModel<IStatistic,TStatistic>, IStatisticViewModel
    where TStatistic : IStatistic
{
    private string _name = string.Empty;
    private string _format = string.Empty;
    private bool _isVisible;
    private Mediator? _mediator;

    /// <inheritdoc/>
    public string Name
    {
        get => _name;
        set => NotifyIfChanged(ref _name, value);
    }

    /// <inheritdoc/>
    public string Format
    {
        get => _format;
        set => NotifyIfChanged(ref _format, value);
    }

    /// <inheritdoc/>
    public bool IsVisible
    {
        get => _isVisible;
        set => NotifyIfChanged(ref _isVisible, value);
    }

    /// <inheritdoc/>
    public Mediator? Mediator
    {
        get => _mediator;
        set => NotifyIfChanged(ref _mediator, value);
    }

    /// <inheritdoc/>
    protected override void OnBinding(TStatistic model)
    {
        Require.NotNull(model, nameof(model));
            
        Name = model.Name;
        Format = model.Format;
        IsVisible = !model.IsHidden;
    }

    /// <inheritdoc/>
    protected override void OnUnbound(TStatistic model)
    {
        Name = string.Empty;
        Format = string.Empty;
        IsVisible = false;
    }
}