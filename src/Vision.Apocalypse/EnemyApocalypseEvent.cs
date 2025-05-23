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
//-----------------------------------------------------------------------s

using System.ComponentModel;
using System.IO;
using BadEcho.Extensions;
using BadEcho.Vision.Apocalypse.Properties;

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides a description for an event generated by the Enemy Apocalypse subsystem in an Omnified game.
/// </summary>
/// <remarks>
/// <para>
/// The Enemy Apocalypse subsystem is executed to handle incoming damage to an enemy from the player. It is
/// not as punishing or exciting (typically) in comparison to what the Player Apocalypse does, but that is
/// inline with the traditional Omnified philosophy (make game harder, not easier, except very rarely).
/// </para>
/// <para>
/// Unlike the more complicated Player Apocalypse subsystem, all possible events that can occur with the Enemy
/// Apocalypse system can be described with this single event type, as all the events revolve around (essentially)
/// some type of bonus multiplier being applied to the damage.
/// </para>
/// </remarks>
public sealed class EnemyApocalypseEvent : ApocalypseEvent
{
    /// <summary>
    /// Gets the additional amount of damage (just the additional amount, not the bonus amount, not the base damage)
    /// inflicted upon the enemy as a result of this event.
    /// </summary>
    public int AdditionalDamage
    { get; init; }

    /// <summary>
    /// Gets the bonus multiplier that was applied to the incoming damage due to this event.
    /// </summary>
    public double BonusMultiplier
    { get; init; }

    /// <summary>
    /// Gets the type of bonus damage being applied by the Enemy Apocalypse event.
    /// </summary>
    public BonusDamageType BonusDamageType
    { get; init; }

    /// <summary>
    /// Gets a value indicating if this Enemy Apocalypse event should be considered as extreme due to how high the
    /// <see cref="BonusMultiplier"/> is regarding the range of possible multiplier values.
    /// </summary>
    public bool IsExtreme
    { get; init; }

    /// <inheritdoc/>
    public override string ToString()
        => DescribeBonus(BonusDamageType, IsExtreme).CulturedFormat(BonusMultiplier, AdditionalDamage);

    /// <inheritdoc/>
    protected override WeightedRandom<Func<Stream>> InitializeSoundMap()
    {
        var soundMap = base.InitializeSoundMap();

        foreach (var (soundAccessor, weight) in GetWeightedSounds(BonusDamageType, IsExtreme))
        {
            soundMap.AddWeight(soundAccessor, weight);
        }

        return soundMap;
    }

    private static string DescribeBonus(BonusDamageType bonusDamageType, bool isExtreme)
        => bonusDamageType switch
        {
            BonusDamageType.CriticalHit => isExtreme ? EffectMessages.ExtremeCriticalHit : EffectMessages.CriticalHit,
            BonusDamageType.Kamehameha => EffectMessages.Kamehameha,
            _ => throw new InvalidEnumArgumentException(nameof(bonusDamageType),
                                                        (int) bonusDamageType,
                                                        typeof(BonusDamageType))
        };

    private static List<(Func<Stream>, int)> GetWeightedSounds(BonusDamageType bonusDamageType, bool isExtreme)
        => bonusDamageType switch
        {
            BonusDamageType.CriticalHit
                => isExtreme
                    ? CreateWeightedSounds((() => EffectSounds.CriticalHitMudbone, 1))
                    : CreateWeightedSounds((() => EffectSounds.CriticalHitChocobo, 2),
                                           (() => EffectSounds.CriticalHitComboBreaker, 1)),
            BonusDamageType.Kamehameha
                => CreateWeightedSounds((() => EffectSounds.Kamehameha, 1)),
            _
                => throw new InvalidEnumArgumentException(nameof(bonusDamageType),
                                                          (int) bonusDamageType,
                                                          typeof(BonusDamageType))
        };

    private static List<(Func<Stream>, int)> CreateWeightedSounds(params (Func<Stream>, int)[] sounds)
    {
        var soundMap = new List<(Func<Stream>, int)>();

        soundMap.AddRange(sounds);

        return soundMap;
    }
}