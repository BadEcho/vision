//-----------------------------------------------------------------------
// <copyright>
//      Created by Matt Weber <matt@badecho.com>
//      Copyright @ 2023 Bad Echo LLC. All rights reserved.
//
//      Bad Echo Technologies are licensed under the
//      GNU Affero General Public License v3.0.
//
//      See accompanying file LICENSE.md or a copy at:
//      https://www.gnu.org/licenses/agpl-3.0.html
// </copyright>
//-----------------------------------------------------------------------

using BadEcho.Presentation.Messaging;
using BadEcho.Presentation.ViewModels;

namespace BadEcho.Vision.Statistics.ViewModels;

/// <summary>
/// Provides a view model that displays statistics exported from an Omnified game.
/// </summary>
internal sealed class StatisticsViewModel : PolymorphicCollectionViewModel<IStatistic,IStatisticViewModel>
{
    private Mediator _mediator;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsViewModel"/> class.
    /// </summary>
    public StatisticsViewModel()
        : this(new Mediator())
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsViewModel"/> class.
    /// </summary>
    /// <param name="mediator">A mediator for messages to be sent or received through.</param>
    public StatisticsViewModel(Mediator mediator)
        : base(new CollectionViewModelOptions {AsyncBatchBindings = false, RemoveChildrenMissingFromBatch = true})
    {
        RegisterDerivation<WholeStatistic, WholeStatisticViewModel>();
        RegisterDerivation<FractionalStatistic, FractionalStatisticViewModel>();
        RegisterDerivation<CoordinateStatistic, CoordinateStatisticViewModel>();
        RegisterDerivation<StatisticGroup, StatisticGroupViewModel>();

        _mediator = mediator;
    }

    /// <inheritdoc/>
    public override IStatisticViewModel CreateChild(IStatistic model)
    {
        IStatisticViewModel viewModel = base.CreateChild(model);

        viewModel.Mediator = _mediator;

        return viewModel;
    }
    
    /// <summary>
    /// Changes the mediator that children use to send or receive messages through.
    /// </summary>
    /// <param name="mediator"></param>
    public void ChangeMediator(Mediator mediator)
    {
        foreach (IStatisticViewModel viewModel in Children)
        {
            viewModel.Mediator = mediator;
        }

        _mediator = mediator;
    }
}