namespace PokerProbabilityCalculator.Evaluator
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PokerProbabilityCalculator.HandRanks;
    interface IEvaluator
    {
        IHandRank GetRank(List<Card> cards);
    }
}
