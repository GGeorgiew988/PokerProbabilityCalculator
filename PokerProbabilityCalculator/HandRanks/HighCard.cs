namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;
    class HighCard : IHandRank
    {
        public HighCard(List<Card> cards)
        {
            this.Cards = new List<Card>(cards);
            this.Strenght = this.Cards.Max(c => c.Value);
            this.Power = 100;
        }
        public HighCard()
        {

        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
    }
}
