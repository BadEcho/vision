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

using BadEcho.Vision.Extensibility;

namespace BadEcho.Vision.Apocalypse;

/// <summary>
/// Provides configuration settings for the Apocalypse Vision module.
/// </summary>
public sealed class ApocalypseModuleConfiguration : VisionModuleConfiguration
{
    /// <summary>
    /// Gets the maximum width constraint of elements responsible for displaying Apocalypse event messages.
    /// </summary>
    public double EffectMessageMaxWidth
    { get; init; } = double.NaN;

    /// <summary>
    /// Gets the number of minutes of inactivity until this module becomes hidden if no new events are received.
    /// </summary>
    public int TimeoutMinutes
    { get; init; }
}