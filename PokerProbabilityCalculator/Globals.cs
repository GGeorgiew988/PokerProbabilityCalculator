namespace PokerProbabilityCalculator
{
    using System.Collections.Generic;
    using System.Linq;
    class Globals
    {
        public static IEnumerable<IGrouping<Card.CardSuits, Card>> GroupBySuit(List<Card> cards)
        {
            var grouped = from c in cards
                          group c by c.Suit;
            return grouped;
        }
        public static IEnumerable<IGrouping<int, Card>> GroupByValue(List<Card> cards)
        {
            var grouped = from c in cards
                          group c by c.Value;
            return grouped.OrderByDescending(g => g.Key);
        }
    }
}
