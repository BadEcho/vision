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

using System.IO;
using BadEcho.Extensions;
using BadEcho.Vision.Apocalypse.Properties;

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides a description for a Player Apocalypse "murder" event that occurred in an Omnified game as the result
/// of a "risk of murder" roll.
/// </summary>
/// <remarks>
/// This event, which is one of the possible outcomes for the dreaded "risk of murder" roll, is what happens when our
/// roll of the die didn't go so well. Known classically as "getting sixty-nined" (due to the traditional damage
/// multiplier of 69x being used), the incoming damage is multiplied to such a high amount that the player will surely
/// be murdered many times over, save for the incredibly rare occurrences of extremely low base damages being applied
/// against mature health pools.
/// </remarks>
public sealed class MurderEvent : RiskOfMurderEvent
{
    /// <summary>
    /// Gets the multiplier applied to the incoming damage amount by the "murder" random effect.
    /// </summary>
    /// <remarks>
    /// This is technically configurable in Omnified hacking framework code, but it has traditionally always been
    /// 69x, and it should always remain so, unless there's a damn good reason!
    /// </remarks>
    public double MurderMultiplier
    { get; init; }

    /// <inheritdoc/>
    public override string ToString()
        => $"{EffectMessages.Murder.CulturedFormat(MurderMultiplier)}{base.ToString()}";

    /// <inheritdoc/>
    protected override WeightedRandom<Func<Stream>> InitializeSoundMap()
    {
        var soundMap = base.InitializeSoundMap();

        soundMap.AddWeight(() => EffectSounds.MurderedHeadshot, 2);
        soundMap.AddWeight(() => EffectSounds.MurderedHolyShit, 2);
        soundMap.AddWeight(() => EffectSounds.MurderedVincentLaugh, 1);

        return soundMap;
    } 
}