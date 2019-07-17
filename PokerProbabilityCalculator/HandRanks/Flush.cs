namespace PokerProbabilityCalculator.HandRanks
{
    using System.Collections.Generic;
    using System.Linq;
    class Flush : IHandRank
    {
        public Flush(List<Card> cards)
        {
            SetCards(cards);
            this.Strenght = this.Cards[0].Value;
            this.Power = 600;
        }
        public List<Card> Cards { get; set; }
        public int Strenght { get; set; }
        public int Power { get; set; }
        private void SetCards(List<Card> cards)
        {
            var grouped = Globals.GroupBySuit(cards);
            foreach (var group in grouped)
            {
                if(group.Count() >= 5)
                {
                    this.Cards = group.OrderByDescending(g => g.Value)
                        .Take(5)
                        .ToList();
                }
            }
        }
    }
}
