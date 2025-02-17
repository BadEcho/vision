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

namespace BadEcho.Vision.Statistics;

/// <summary>
/// Defines an individual statistic exported from an Omnified game.
/// </summary>
public interface IStatistic
{
    /// <summary>
    /// Gets the display name of the statistic.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets any formatting that should be applied to the value of the statistic.
    /// </summary>
    string Format { get; }

    /// <summary>
    /// Gets a value indicating if the statistic should be hidden from view, while maintaining its place among
    /// its brethren.
    /// </summary>
    bool IsHidden { get; }
}