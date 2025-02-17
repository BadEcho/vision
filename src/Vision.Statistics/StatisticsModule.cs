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

using System.Composition;
using System.Text.Json;
using BadEcho.Extensibility.Hosting;
using BadEcho.Vision.Extensibility;
using BadEcho.Vision.Statistics.ViewModels;

namespace BadEcho.Vision.Statistics;

/// <summary>
/// Provides a snap-in module granting vision to Omnified game statistics data.
/// </summary>
[Export(typeof(IVisionModule))]
internal sealed class StatisticsModule : VisionModule<IStatistic, StatisticsViewModel>
{
    private const string DEPENDENCY_NAME 
        = nameof(StatisticsModule) + nameof(LocalDependency);

    private static readonly JsonSerializerOptions _StatisticOptions = new()
                                                                      {
                                                                          Converters = { new StatisticConverter() }
                                                                      };
    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsModule"/> class.
    /// </summary>
    /// <param name="configuration">Configuration settings for the Vision application.</param>
    [ImportingConstructor]
    public StatisticsModule([Import(DEPENDENCY_NAME)] IVisionConfiguration configuration)
        : base(configuration)
    { }

    /// <inheritdoc/>
    public override string MessageFile
        => "statistics.json";

    /// <inheritdoc/>
    protected override AnchorPointLocation DefaultLocation
        => AnchorPointLocation.TopLeft;

    /// <inheritdoc/>
    protected override StatisticsViewModel InitializeViewModel()
    {
        var viewModel = new StatisticsViewModel(Configuration.Mediator);

        if (Configuration.Dispatcher != null)
            viewModel.ChangeDispatcher(Configuration.Dispatcher);

        return viewModel;
    }

    protected override IEnumerable<IStatistic>? ParseMessages(string messages) 
        => JsonSerializer.Deserialize<IEnumerable<IStatistic>>(messages, _StatisticOptions);

    /// <summary>
    /// Provides a convention provider that allows for an armed context in which this module can have its required configuration
    /// provided to it during its initialization and exportation.
    /// </summary>
    /// <suppressions>
    /// ReSharper disable ClassNeverInstantiated.Local
    /// </suppressions>
    [Export(typeof(IConventionProvider))]
    private sealed class LocalDependency : DependencyRegistry<IVisionConfiguration>
    {
        public LocalDependency()
            : base(DEPENDENCY_NAME)
        { }

        /// <inheritdoc />
        public override IVisionConfiguration Dependency
            => LoadDependency();
    }
}