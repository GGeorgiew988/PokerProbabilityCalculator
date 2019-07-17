namespace PokerProbabilityCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PokerProbabilityCalculator.Evaluator;
    using PokerProbabilityCalculator.HandRanks;
    class Poker
    {
        const int tableHeight = 20;
        const int tableWidth = 120;
        const int minPlayers = 2;
        const int maxPlayers = 8;
        static List<int[]> playerCoordinates = new List<int[]>
            {
                new int[] {2,35} ,new int[] {15,65}, new int[] {2,65}, new int[] {15,35},
                new int[] {2,5} ,new int[] {15,95}, new int[] {2,95}, new int[] {15,5}
            };
        static List<int[]> CardCoordinates = new List<int[]>
            {
                new int[] {9,35} ,new int[] {9,45}, new int[] {9,55}, new int[] {9,65},new int[] {9,75}
            };
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                Console.WriteLine("Welcome to Console Poker\n");
                char[,] table = new char[tableHeight, tableWidth];
                Dealer dealer = new Dealer();
                IEvaluator evaluator = new TexasEvaluator();
                List<Player> players = AddPlayers();
                List<Card> cardsOnTable = new List<Card>();

                RefreshTable(table, players, cardsOnTable);
                Wait();

                dealer.Deal(players);
                EvaluateHands(players, cardsOnTable, evaluator);
                RefreshTable(table, players, cardsOnTable);
                Wait();

                dealer.OpenFlop(cardsOnTable);
                EvaluateHands(players, cardsOnTable, evaluator);
                ProbabilityCalculator.Calculate(players, cardsOnTable, dealer.Deck);
                RefreshTable(table, players, cardsOnTable);
                Wait();

                dealer.OpenTurnOrRiver(cardsOnTable);
                EvaluateHands(players, cardsOnTable, evaluator);
                ProbabilityCalculator.Calculate(players, cardsOnTable, dealer.Deck);
                RefreshTable(table, players, cardsOnTable);
                Wait();

                dealer.OpenTurnOrRiver(cardsOnTable);
                EvaluateHands(players, cardsOnTable, evaluator);
                ProbabilityCalculator.Calculate(players, cardsOnTable, dealer.Deck);
                RefreshTable(table, players, cardsOnTable);
                DisplayWinner(players);
                Wait();
                Console.Clear();
            }
        }
        private static List<Player> AddPlayers()
        {
            List<Player> players = new List<Player>();
            while (true)
            {
                Console.Write("Player names (2-8 separated by a space): ");
                //string[] names = "gosho pesho dani mitko simo marti niks pan4o"
                string[] names = Console.ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Distinct()
                    .ToArray();
                bool playersAreTooMany = names.Length > maxPlayers;
                bool playersAreTooFew = names.Length < minPlayers;

                if (playersAreTooFew ||
                    playersAreTooMany)
                {
                    Console.WriteLine("Too much or too few players added! Try Again.");
                    continue;
                }
                else
                {
                    foreach (var name in names)
                    {
                        players.Add(new Player(name));
                    }
                    break;
                }
            }
            return players;
        }
        private static void EvaluateHands(List<Player> players, List<Card> cards, IEvaluator eval)
        {
            foreach (var player in players)
            {
                player.Evaluate(cards, eval);
            }
        }
        private static void RefreshTable(char[,] table, List<Player> players, List<Card> tableCards)
        {
            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableWidth; j++)
                {
                    table[i, j] = ' ';
                }
            }
            RefreshCards(table, tableCards);
            RefreshPlayers(table, players);
            DisplayTable(table);
        }
        private static void RefreshCards(char[,] table, List<Card> tableCards)
        {
            for (int i = 0; i < tableCards.Count; i++)
            {
                PlaceAt(tableCards[i], CardCoordinates[i], table);
            }
        }
        private static void RefreshPlayers(char[,] table, List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                PlaceAt(players[i], playerCoordinates[i], table);
            }
        }
        private static void DisplayTable(char[,] table)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for (int i = 0; i < tableHeight; i++)
            {
                for (int j = 0; j < tableWidth; j++)
                {
                    if (table[i, j] == '♥' ||
                        table[i, j] == '♦')
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(table[i, j]);
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void PlaceAt(Card card, int[] coordinates, char[,] table)
        {
            string cardStr = card.ToString();
            for (int i = 0; i < cardStr.Length; i++)
            {
                table[coordinates[0], coordinates[1] + i] = cardStr[i];
            }
        }
        private static void PlaceAt(Player player, int[] coordinates, char[,] table)
        {
            bool playerHasFullHand = player.Hand.Count == 2;

            WritePlayerName(player, coordinates, table);
            if (playerHasFullHand)
            {
                PlacePlayerHand(player, coordinates, table);
            }
            WritePlayerHandType(player, coordinates, table);
            WritePlayerWinProbability(player, coordinates, table);
            WritePlayerTieProbability(player, coordinates, table);
        }
        private static void WritePlayerWinProbability(Player player, int[] coordinates, char[,] table)
        {
            string probabilityStr = "Win: " + player.ProbabilityWin.ToString() + "%";
            for (int i = 0; i < probabilityStr.Length; i++)
            {
                table[coordinates[0] + 3, coordinates[1] + i] = probabilityStr[i];
            }
        }
        private static void WritePlayerTieProbability(Player player, int[] coordinates, char[,] table)
        {
            string probabilityStr = "Tie: " + player.ProbabilityTie.ToString() + "%";
            for (int i = 0; i < probabilityStr.Length; i++)
            {
                table[coordinates[0] + 4, coordinates[1] + i] = probabilityStr[i];
            }
        }
        private static void WritePlayerHandType(Player player, int[] coordinates, char[,] table)
        {
            string handType = player.GiveHandType();
            const int spacesBeforeName = 3;
            for (int i = 0; i < handType.Length; i++)
            {
                table[coordinates[0] + 2, coordinates[1] - spacesBeforeName + i] = handType[i];
            }
        }
        private static void PlacePlayerHand(Player player, int[] coordinates, char[,] table)
        {
            PlaceAt(player.Hand[0], new int[] { coordinates[0] + 1, coordinates[1] }, table);
            PlaceAt(player.Hand[1], new int[] { coordinates[0] + 1, coordinates[1] + 4 }, table);
        }
        private static void WritePlayerName(Player player, int[] coordinates, char[,] table)
        {
            for (int i = 0; i < player.Name.Length; i++)
            {
                table[coordinates[0], coordinates[1] + i] = player.Name[i];
            }
        }
        private static void DisplayWinner(List<Player> players)
        {
            List<Player> winners = WinDeterminator.FindWinner(players);
            if (winners.Count > 1)
                Console.WriteLine("Tie Between: ");
            else
                Console.WriteLine("THE WINNER IS ");
            foreach (var winner in winners)
           {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.Write(winner.Name);
               Console.ForegroundColor = ConsoleColor.White;
               Console.Write(" with ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(winner.GiveHandType());
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void Wait()
        {
            Console.Write("Press Enter to continue: ");
            while (true)
            {
                char input = (char)Console.ReadKey().KeyChar;
                if (input == '\r')
                    break;
            }
        }
    }
}
