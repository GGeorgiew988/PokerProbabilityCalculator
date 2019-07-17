namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;

    class Pair : IHandRank
    {
        public Pair(List<Card> cards)
        {
            this.Cards = new List<Card>();
            SetCards(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 200;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            int twoPairValue = AddPairs(grouped);
            AddBiggestThreeCards(cards, twoPairValue);
        }
        private void AddBiggestThreeCards(List<Card> cards, int twoPairValue)
        {
            int added = 0;
            cards = cards.OrderByDescending(c => c.Value).ToList();
            for (int i = 0; added < 3; i++)
            {
                if (i >= cards.Count)
                    break;
                if (cards[i].Value == twoPairValue)
                    continue;
                else
                {
                    this.Cards.Add(cards[i]);
                    added++;
                }
            }
        }
        private int AddPairs(IEnumerable<IGrouping<int, Card>> grouped)
        {
            int twoPairValue = 0;
            foreach (var group in grouped)
            {
                if (group.Count() == 2)
                {
                    this.Cards.AddRange(group.Take(2).ToList());
                    twoPairValue = this.Cards[0].Value;
                }
            }

            return twoPairValue;
        }
    }
}
