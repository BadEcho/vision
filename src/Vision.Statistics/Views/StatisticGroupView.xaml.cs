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

using System.Windows;

namespace BadEcho.Vision.Statistics.Views;

/// <summary>
/// Provides a view for a grouping of similar statistics exported from an Omnified game.
/// </summary>
public partial class StatisticGroupView
{
    /// <summary>
    /// Identifies the attached property which indicates whether the statistic should be displayed in compact form.
    /// </summary>
    public static readonly DependencyProperty IsCompactProperty
        = StatisticLayout.IsCompactProperty.AddOwner(typeof(StatisticGroupView),
                                                     new FrameworkPropertyMetadata(
                                                         false,
                                                         FrameworkPropertyMetadataOptions.Inherits));
    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticGroupView"/> class.
    /// </summary>
    public StatisticGroupView() 
        => InitializeComponent();
}