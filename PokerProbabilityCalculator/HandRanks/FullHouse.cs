namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;

    class FullHouse : IHandRank
    {
        public FullHouse(List<Card> cards)
        {
            this.Cards = new List<Card>();
            SetCards(cards);
            SetStrenght(this.Cards);
            this.Power = 700;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
        private void SetStrenght(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            foreach (var group in grouped)
            {
                if (group.Count() == 3)
                    this.Strenght = group.First().Value;
            }
        }
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            foreach (var group in grouped)
            {
                if(group.Count() == 3)
                {
                    this.Cards.AddRange(group.Take(3).ToList());
                }
                else if(group.Count() == 2)
                {
                    this.Cards.AddRange(group.Take(2).ToList());
                }
            }
        }
    }
}
