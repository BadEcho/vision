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

using System.ComponentModel;
using System.Text.Json;
using BadEcho.Serialization;
using BadEcho.Vision.Statistics.Properties;

namespace BadEcho.Vision.Statistics;

/// <summary>
/// Provides a converter of <see cref="Statistic"/> objects to or from JSON.
/// </summary>
public sealed class StatisticConverter : JsonPolymorphicConverter<StatisticType,IStatistic>
{
    private static readonly JsonSerializerOptions _StatisticOptions = new()
                                                                      {
                                                                          Converters = { new StatisticConverter() }
                                                                      };
    /// <inheritdoc/>
    protected override string DataPropertyName
        => "Statistic";

    /// <inheritdoc/>
    protected override IStatistic? ReadFromDescriptor(ref Utf8JsonReader reader, StatisticType typeDescriptor)
    {
        return typeDescriptor switch
        {
            StatisticType.Whole => JsonSerializer.Deserialize<WholeStatistic>(ref reader),
            StatisticType.Fractional => JsonSerializer.Deserialize<FractionalStatistic>(ref reader),
            StatisticType.Coordinate => JsonSerializer.Deserialize<CoordinateStatistic>(ref reader),
            StatisticType.Group => JsonSerializer.Deserialize<StatisticGroup>(ref reader, _StatisticOptions),
            _ => throw new InvalidEnumArgumentException(nameof(typeDescriptor), 
                                                        (int) typeDescriptor, 
                                                        typeof(StatisticType))
        };
    }

    /// <inheritdoc/>
    protected override StatisticType DescriptorFromValue(IStatistic value)
    {
        return value switch
        {
            WholeStatistic => StatisticType.Whole,
            FractionalStatistic => StatisticType.Fractional,
            CoordinateStatistic => StatisticType.Coordinate,
            StatisticGroup => StatisticType.Group,
            _ => throw new ArgumentException(Strings.StatisticTypeUnsupportedJson,
                                             nameof(value))
        };
    }
}