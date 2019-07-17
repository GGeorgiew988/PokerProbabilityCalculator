namespace PokerProbabilityCalculator.Evaluator
{
    using System.Collections.Generic;
    using System.Linq;
    using PokerProbabilityCalculator.HandRanks;
    class TexasEvaluator : IEvaluator
    {
        public IHandRank GetRank(List<Card> playerCards)
        {
            List<Card> cards = new List<Card>(playerCards);
            if (IsRoyalFlush(cards))
                return new RoyalFlush();
            else if (IsStraightFlush(cards))
                return new StraightFlush(cards);
            else if (IsFourOfKind(cards))
                return new FourOfKind(cards);
            else if (IsFullHouse(cards))
                return new FullHouse(cards);
            else if (IsFlush(cards))
                return new Flush(cards);
            else if (IsStraight(cards))
                return new Straight(cards);
            else if (IsThreeOfKind(cards))
                return new ThreeOfKind(cards);
            else if (IsTwoPair(cards))
                return new TwoPair(cards);
            else if (IsPair(cards))
                return new Pair(cards);
            else
                return new HighCard(cards);

        }
        private bool IsRoyalFlush(List<Card> cards)
        {
            var groups = Globals.GroupBySuit(cards);
            foreach (var group in groups)
            {
                var cardList = group.ToList();
                if (cardList.Count == 5 &&
                    cardList.Exists(c => c.Value == (int)Card.cardValues.Ace) &&
                    cardList.Exists(c => c.Value == (int)Card.cardValues.King) &&
                    cardList.Exists(c => c.Value == (int)Card.cardValues.Qeen) &&
                    cardList.Exists(c => c.Value == (int)Card.cardValues.Jack) &&
                    cardList.Exists(c => c.Value == 10))
                    return true;
            }
            return false;
        }
        private bool IsStraightFlush(List<Card> cards)
        {
            var grouped = Globals.GroupBySuit(cards);
            foreach (var group in grouped)
            {
                List<Card> straight = group.ToList();
                if (HasNumberOfCards(group, 5) &&
                    IsStraight(straight))
                {
                    cards = straight;
                    return true;
                }
            }
            return false;
        }
        private bool IsFourOfKind(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            foreach (var group in grouped)
            {
                if (HasExactNumberOfCards(group, 4))
                    return true;
            }
            return false;
        }
        private bool IsFullHouse(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            int numberOfThreeOfKind = 0;
            int numberOfPairs = 0;
            foreach (var group in grouped)
            {
                if (HasExactNumberOfCards(group, 3))
                    numberOfThreeOfKind++;
                else if (HasExactNumberOfCards(group, 2))
                    numberOfPairs++;
            }
            if (numberOfThreeOfKind == 2 ||
              (numberOfThreeOfKind == 1 && numberOfPairs >= 1))
            {
                return true;
            }
            else
                return false;
        }
        private bool IsFlush(List<Card> cards)
        {
            var grouped = Globals.GroupBySuit(cards);
            foreach (var group in grouped)
            {
                if (HasNumberOfCards(group, 5))
                {
                    List<Card> ordered = group.OrderByDescending(x => x.Value).ToList();
                    return true;
                }
            }
            return false;
        }
        private bool IsStraight(List<Card> cards)
        {
            List<int> ordered = cards.Select(x => x.Value).Distinct().OrderByDescending(x => x).ToList();
            if (ordered.Count < 5)
                return false;

            const int firstThreeCards = 3;
            for (int i = 0; i < firstThreeCards; i++)
            {
                bool goesOutOfRange = i + 4 >= ordered.Count;
                if (goesOutOfRange)
                    break;
                if (FoundStraight(ordered, i))
                {
                    cards = cards.Take(5).ToList();
                    return true;
                }
            }
            if (StraightIsToFive(ordered))
            {
                return true;
            }
            else
                return false;
        }
        private static bool StraightIsToFive(List<int> ordered)
        {
            if (ordered[0] == (int)Card.cardValues.Ace)
            {
                if (ordered.Contains(2) &&
                    ordered.Contains(3) &&
                    ordered.Contains(4) &&
                    ordered.Contains(5))
                    return true;
            }
            return false;
        }
        private static bool FoundStraight(List<int> ordered, int i)
        {
            int firstCardValue = ordered[i];
            int secondCardValue = ordered[i + 1];
            int thirdCardValue = ordered[i + 2];
            int fourthCardValue = ordered[i + 3];
            int fifthCardValue = ordered[i + 4];

            if (firstCardValue - 1 == secondCardValue &&
               secondCardValue - 1 == thirdCardValue &&
               thirdCardValue - 1 == fourthCardValue &&
               fourthCardValue - 1 == fifthCardValue)
                return true;
            else return false;
        }
        private bool IsThreeOfKind(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            foreach (var group in grouped)
            {
                if (HasExactNumberOfCards(group, 3))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsTwoPair(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            List<int> pairs = new List<int>();
            foreach (var group in grouped)
            {
                AddPairs(pairs, group);
            }
            if (pairs.Count < 2)
                return false;
            else
            {
                return true;
            }
        }
        private static void AddPairs(List<int> pairs, IGrouping<int, Card> group)
        {
            if (HasExactNumberOfCards(group, 2))
            {
                pairs.Add(group.First().Value);
            }
        }
        private bool IsPair(List<Card> cards)
        {
            var grouped = Globals.GroupByValue(cards);
            foreach (var group in grouped)
            {
                if (HasExactNumberOfCards(group, 2))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasNumberOfCards<T>(IGrouping<T, Card> group, int count)
        {
            if (group.Count() >= count)
                return true;
            else
                return false;
        }
        private static bool HasExactNumberOfCards<T>(IGrouping<T, Card> group, int count)
        {
            if (group.Count() == count)
                return true;
            else
                return false;
        }
    }
}
