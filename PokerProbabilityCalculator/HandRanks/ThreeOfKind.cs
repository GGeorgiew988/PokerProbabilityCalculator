namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;

    class ThreeOfKind : IHandRank
    {
        public ThreeOfKind(List<Card> cards)
        {
            this.Cards = new List<Card>();
            SetCards(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 400;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            int threeOfKindValue = AddThreeOfKind(grouped);
            AddBiggestTwoCards(cards, threeOfKindValue);
        }
        private void AddBiggestTwoCards(List<Card> cards, int threeOfKindValue)
        {
            int added = 0;
            cards = cards.OrderByDescending(c => c.Value).ToList();
            for (int i = 0; added < 2; i++)
            {
                if (i >= cards.Count)
                    break;
                if (cards[i].Value == threeOfKindValue)
                    continue;
                else
                {
                    this.Cards.Add(cards[i]);
                    added++;
                }
            }
        }
        private int AddThreeOfKind(IEnumerable<IGrouping<int, Card>> grouped)
        {
            int threeOfKindValue = 0;
            foreach (var group in grouped)
            {
                if (group.Count() == 3)
                {
                    this.Cards.AddRange(group.Take(3).ToList());
                    threeOfKindValue = this.Cards[0].Value;
                }
            }

            return threeOfKindValue;
        }
    }
}
