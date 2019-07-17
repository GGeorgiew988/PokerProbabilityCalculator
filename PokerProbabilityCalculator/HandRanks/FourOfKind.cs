namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;
    class FourOfKind : IHandRank
    {
        public FourOfKind(List<Card> cards)
        {
            SetCards(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 800;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; } = 800;
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            int fourOfKindValue = 0;
            foreach (var group in grouped)
            {
                if (group.Count() == 4)
                {
                    this.Cards = group.OrderByDescending(g => g.Value)
                        .Take(4)
                        .ToList();
                    fourOfKindValue = this.Cards[0].Value;
                }
            }
            this.Cards.Add(cards.OrderByDescending(c => c.Value).First());
        }
    }
}
