namespace PokerProbabilityCalculator
{
    using System.Collections.Generic;
    public class Card
    {
        public enum cardValues
        {
            Jack = 11,
            Qeen = 12,
            King = 13,
            Ace = 14
        }
        public enum CardSuits
        {
            Spades,
            Clubs,
            Diamonds,
            Hearts
        }
        public Card(int value, CardSuits suit)
        {
            this.Value = value;
            this.Suit = suit;
        }
        public int Value { get; set; }
        public CardSuits Suit { get; set; }
        public override string ToString()
        {
            Dictionary<int, string> cards = new Dictionary<int, string>
            { {2,"2"},{3,"3"},{4,"4"},{5,"5"},{6,"6"},{7,"7"},{8,"8"},{9,"9"},{10,"10" },{11,"J"},{12,"Q"},{13,"K"},{14,"A"}};
            char suit = '\0';
            switch (this.Suit)
            {
                case CardSuits.Spades:
                    suit = '♠';
                    break;
                case CardSuits.Hearts:
                    suit = '♥';
                    break;
                case CardSuits.Clubs:
                    suit = '♣';
                    break;
                case CardSuits.Diamonds:
                    suit = '♦';
                    break;
                default:
                    break;
            }
            return cards[this.Value] + suit;
        }
    }
}
