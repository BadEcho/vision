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

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides a base description for a Player Apocalypse "risk of murder" event that occurred in an Omnified game.
/// </summary>
/// <remarks>
/// A risk of murder event is special kind of random effect that results in an additional, 5-sided die to be rolled.
/// This type of event is so named because, depending on how the die lands, death upon the player may become all but
/// guaranteed.
/// </remarks>
public abstract class RiskOfMurderEvent : PlayerApocalypseEvent
{
    /// <summary>
    /// Gets the risk of murder die roll for this Player Apocalypse event, which acts as the main determinant in whether
    /// the player is murdered absolutely.
    /// </summary>
    public int MurderRoll
    { get; init; }
}