namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    class RoyalFlush : IHandRank
    {
        public RoyalFlush()
        {
            this.Cards = new List<Card>();
            this.Power = 1000;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
    }
}
