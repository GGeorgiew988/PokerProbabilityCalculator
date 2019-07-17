namespace PokerProbabilityCalculator
{
    using System;
    using System.Collections.Generic;
    class Dealer
    {
        public Dealer()
        {
            this.Deck = new List<Card>();
            const int allCardValuesCount = 13;

            for (int i = 2; i <= allCardValuesCount + 1; i++)
            {
                Card newCard = new Card(i, Card.CardSuits.Spades);
                this.Deck.Add(newCard);
                newCard = new Card(i, Card.CardSuits.Clubs);
                this.Deck.Add(newCard);
                newCard = new Card(i, Card.CardSuits.Diamonds);
                this.Deck.Add(newCard);
                newCard = new Card(i, Card.CardSuits.Hearts);
                this.Deck.Add(newCard);
            }
        }
        public List<Card> Deck { get; set; }
        public void Deal(List<Player> players)
        {
            Random rnd = new Random();
            foreach (var player in players)
            {
                for (int i = 0; i < 2; i++)
                {
                    int random = rnd.Next(this.Deck.Count - 1);
                    Card newCard = this.Deck[random];
                    player.Hand.Add(newCard);
                    Deck.Remove(newCard);
                }
            }
        }
        public void OpenFlop(List<Card> tableCards)
        {
            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                int random = rnd.Next(this.Deck.Count - 1);
                Card newCard = this.Deck[random];
                tableCards.Add(newCard);
                Deck.Remove(newCard);
            }
        }
        public void OpenTurnOrRiver(List<Card> tableCards)
        {
            Random rnd = new Random();
            int random = rnd.Next(this.Deck.Count - 1);
            Card newCard = this.Deck[random];
            tableCards.Add(newCard);
            Deck.Remove(newCard);
        }
    }
}
