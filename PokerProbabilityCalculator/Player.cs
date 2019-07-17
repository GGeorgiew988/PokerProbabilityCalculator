namespace PokerProbabilityCalculator
{
    using System.Collections.Generic;
    using System.Text;
    using PokerProbabilityCalculator.Evaluator;
    using PokerProbabilityCalculator.HandRanks;
    class Player
    {
        public Player(string name)
        {
            this.Name = name;
            this.Hand = new List<Card>();
            this.HandRank = new HighCard();
        }
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public IHandRank HandRank { get; set; }
        public double ProbabilityWin { get; set; }
        public double ProbabilityTie { get; set; }
        public void Evaluate(List<Card> tableCards, IEvaluator eval)
        {
            this.HandRank.Cards?.Clear();
            this.HandRank = eval.GetRank(JoinHandWithTableCards(tableCards));
        }
        private List<Card> JoinHandWithTableCards(List<Card> tableCards)
        {
            List<Card> joined = new List<Card>();
            foreach (var card in this.Hand)
            {
                joined.Add(card);
            }
            foreach (var card in tableCards)
            {
                joined.Add(card);
            }
            return joined;
        }
        public string GiveHandType()
        {
            StringBuilder sb = new StringBuilder();
            if (this.HandRank is RoyalFlush)
                sb.Append("Royal flush ");
            if (this.HandRank is StraightFlush)
                sb.Append("Straight flush ");
            if (this.HandRank is FourOfKind)
                sb.Append("Four of a kind ");
            if (this.HandRank is FullHouse)
                sb.Append("Full House ");
            if (this.HandRank is Flush)
                sb.Append("Flush ");
            if (this.HandRank is Straight)
                sb.Append("Straight ");
            if (this.HandRank is ThreeOfKind)
                sb.Append("Three of a kind ");
            if (this.HandRank is TwoPair)
                sb.Append("Two pair ");
            if (this.HandRank is Pair)
                sb.Append("Pair ");
                if (this.HandRank is HighCard)
                sb.Append("High card ");

            switch (this.HandRank.Strenght)
            {
                case 2:
                    sb.Append("Deuce");
                    break;
                case 3:
                    sb.Append("Three");
                    break;
                case 4:
                    sb.Append("Four");
                    break;
                case 5:
                    sb.Append("Five");
                    break;
                case 6:
                    sb.Append("Six");
                    break;
                case 7:
                    sb.Append("Seven");
                    break;
                case 8:
                    sb.Append("Eight");
                    break;
                case 9:
                    sb.Append("Nine");
                    break;
                case 10:
                    sb.Append("Ten");
                    break;
                case 11:
                    sb.Append("Jack");
                    break;
                case 12:
                    sb.Append("Queen");
                    break;
                case 13:
                    sb.Append("King");
                    break;
                case 14:
                    sb.Append("Ace");
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }
    }
}
