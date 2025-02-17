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

namespace BadEcho.Vision.Apocalypse.ViewModels;

/// <summary>
/// Provides a view model that facilitates the display of an event generated by the Player Apocalypse subsystem in
/// an Omnified game.
/// </summary>
/// <typeparam name="TPlayerApocalypseEvent">
/// The type of Player Apocalypse event bound to the view model for display on a view.
/// </typeparam>
internal class PlayerApocalypseEventViewModel<TPlayerApocalypseEvent> : ApocalypseEventViewModel<TPlayerApocalypseEvent>,
                                                                        IPlayerApocalypseEventViewModel
    where TPlayerApocalypseEvent : PlayerApocalypseEvent
{
    private int _dieRoll;

    /// <inheritdoc/>
    public int DieRoll
    {
        get => _dieRoll;
        set => NotifyIfChanged(ref _dieRoll, value);
    }

    /// <inheritdoc/>
    protected override void OnBinding(TPlayerApocalypseEvent model)
    {
        base.OnBinding(model);

        DieRoll = model.DieRoll;
    }

    /// <inheritdoc/>
    protected override void OnUnbound(TPlayerApocalypseEvent model)
    {
        base.OnUnbound(model);

        DieRoll = default;
    }
}