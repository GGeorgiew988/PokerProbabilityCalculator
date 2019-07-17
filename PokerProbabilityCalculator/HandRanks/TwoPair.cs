namespace PokerProbabilityCalculator.HandRanks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    class TwoPair : IHandRank
    {
        public TwoPair(List<Card> cards)
        {
            this.Cards = new List<Card>();
            SetCards(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 300;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards.OrderByDescending(c => c.Value).ToList());
            AddPairs(grouped);
            AddHighestCard(cards);
        }
        private void AddHighestCard(List<Card> cards)
        {
            int addedCount = 0;
            cards = cards.OrderByDescending(c => c.Value).ToList();
            for (int i = 0; addedCount < 1; i++)
            {
                if (i >= cards.Count)
                    break;
                if (cards[i].Value == this.Cards[0].Value ||
                     cards[i].Value == this.Cards[1].Value)
                    continue;
                else
                {
                    this.Cards.Add(cards[i]);
                    addedCount++;
                }
            }
        }
        private int AddPairs(IEnumerable<IGrouping<int, Card>> grouped)
        {
            int addedCount = 0;
            foreach (var group in grouped)
            {
                if (group.Count() == 2)
                {
                    addedCount++;
                    this.Cards.AddRange(group.Take(2));
                }
                if (addedCount == 2)
                    break;
            }

            return addedCount;
        }
    }
}
