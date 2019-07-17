namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;

    class Straight : IHandRank
    {
        public Straight(List<Card> cards)
        {
            this.Cards = new List<Card>(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 500;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
    }
}
