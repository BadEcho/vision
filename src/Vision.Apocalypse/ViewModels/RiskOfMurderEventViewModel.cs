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
/// Provides a view model that facilitates the display of a "risk of murder" event generated by the Player Apocalypse
/// subsystem in an Omnified game.
/// </summary>
internal sealed class RiskOfMurderEventViewModel : PlayerApocalypseEventViewModel<RiskOfMurderEvent>
{
    private int _murderRoll;

    /// <summary>
    /// Gets or sets the risk of murder die roll for the bound Player Apocalypse event.
    /// </summary>
    public int MurderRoll
    {
        get => _murderRoll;
        set => NotifyIfChanged(ref _murderRoll, value);
    }

    /// <inheritdoc/>
    protected override void OnBinding(RiskOfMurderEvent model)
    {
        base.OnBinding(model);

        MurderRoll = model.MurderRoll;
    }

    /// <inheritdoc/>
    protected override void OnUnbound(RiskOfMurderEvent model)
    {
        base.OnUnbound(model);

        MurderRoll = default;
    }
}