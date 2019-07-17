namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;

    class StraightFlush : IHandRank
    {
        public StraightFlush(List<Card> cards)
        {
            this.Cards = new List<Card>(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 900;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
    }
}
