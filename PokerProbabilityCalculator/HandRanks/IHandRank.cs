namespace PokerProbabilityCalculator.HandRanks
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    interface IHandRank
    {
       List<Card> Cards { get; set; }
       int Strenght { get; set; }
        int Power { get; set; }
    }
}
