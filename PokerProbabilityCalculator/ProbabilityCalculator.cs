namespace PokerProbabilityCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PokerProbabilityCalculator.Evaluator;

    static class ProbabilityCalculator
    {

        public static void Calculate(List<Player> players, List<Card> tableCards, List<Card> remainingDeck)
        {
            IEvaluator eval = new TexasEvaluator();
            List<Player> testPlayers = new List<Player>();
            foreach (var player in players)
            {
                Player newPlayer = new Player(player.Name);
                newPlayer.Hand.Add(player.Hand[0]);
                newPlayer.Hand.Add(player.Hand[1]);
                testPlayers.Add(newPlayer);
            }
            Dictionary<string, int> playerWins = new Dictionary<string, int>();
            Dictionary<string, int> playerDraws = new Dictionary<string, int>();
            InitializeDicts(testPlayers, playerWins, playerDraws);
            int trials = 0;

            List<Card> testTableCards = new List<Card>(tableCards);
            List<Card> testRemainingDeck = new List<Card>(remainingDeck);
            if (testTableCards.Count == 3)
                TestForFlop(eval, testPlayers, playerWins, playerDraws, ref trials, testTableCards, testRemainingDeck ,players);

            else if (testTableCards.Count == 4)
                TestForTurn(eval, testPlayers, playerWins, playerDraws, ref trials, testTableCards, testRemainingDeck);

            else if(testTableCards.Count == 5)
                TestForRiver(eval, testPlayers, playerWins, playerDraws, ref trials, testTableCards);

            SetProbability(players, playerWins, playerDraws, trials);
        }
        private static void TestForTurn(IEvaluator eval, List<Player> testPlayers, Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws, ref int trials, List<Card> testTableCards, List<Card> testRemainingDeck)
        {
            trials = 0;
            for (int i = 0; i < testRemainingDeck.Count; i++)
            {
                Card testCard = testRemainingDeck[i];
                testTableCards.Add(testCard);
                EvaluatePlayers(eval, testPlayers, testTableCards);
                List<Player> winners = WinDeterminator.FindWinner(testPlayers);
                trials++;
                IncrementDicts(playerWins, playerDraws, winners);
                testTableCards.Remove(testCard);
            }
        }
        private static void TestForFlop(IEvaluator eval, List<Player> testPlayers, Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws, ref int trials, List<Card> testTableCards, List<Card> testRemainingDeck, List<Player> players)
        {
            while(testRemainingDeck.Count != 0)
            {
                Card first = testRemainingDeck[0];
                Card second;
                testTableCards.Add(first);
                testRemainingDeck.Remove(first);

                for (int i = 0; i < testRemainingDeck.Count; i++)
                {
                    second = testRemainingDeck[i];
                    testTableCards.Add(second);
                    EvaluatePlayers(eval, testPlayers, testTableCards);
                    List<Player> winners = WinDeterminator.FindWinner(testPlayers);
                    trials++;
                    IncrementDicts(playerWins, playerDraws, winners);
                    testTableCards.Remove(second);
                }
                testTableCards.Remove(first);
            }
        }
        private static void TestForRiver(IEvaluator eval, List<Player> testPlayers, Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws, ref int trials, List<Card> testTableCards)
        {
            trials = 0;
            EvaluatePlayers(eval, testPlayers, testTableCards);
            List<Player> winners = WinDeterminator.FindWinner(testPlayers);
            trials++;
            IncrementDicts(playerWins, playerDraws, winners);      
        }
        private static void IncrementDicts(Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws, List<Player> winners)
        {
            if (winners.Count == 1)
                playerWins[winners[0].Name]++;
            else
                foreach (var player in winners)
                {
                    playerDraws[player.Name]++;
                }
        }
        private static void EvaluatePlayers(IEvaluator eval, List<Player> testPlayers, List<Card> testTableCards)
        {
            foreach (var player in testPlayers)
            {
                player.Evaluate(testTableCards, eval);
            }
        }
        private static void InitializeDicts(List<Player> testPlayers, Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws)
        {
            foreach (var player in testPlayers)
            {
                playerWins.Add(player.Name, 0);
            }
            foreach (var player in testPlayers)
            {
                playerDraws.Add(player.Name, 0);
            }
        }
        private static void SetProbability(List<Player> players, Dictionary<string, int> playerWins, Dictionary<string, int> playerDraws, int trials)
        {
            foreach (var kvp in playerWins)
            {
                double probability = (double)kvp.Value * 100 / trials;
                probability = Math.Round(probability, 2);
                players.Find(p => p.Name == kvp.Key).ProbabilityWin = probability;
            }
            
            foreach (var kvp in playerDraws)
            {
                double probability = (double)kvp.Value * 100 / trials;
                probability = Math.Round(probability, 2);
                players.Find(p => p.Name == kvp.Key).ProbabilityTie = probability;
            }
        }
    }
}
