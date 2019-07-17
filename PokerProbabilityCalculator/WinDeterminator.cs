namespace PokerProbabilityCalculator
{
    using System.Collections.Generic;
    using System.Linq;
    static class WinDeterminator
    {
        public static List<Player> FindWinner(List<Player> players)
        {
            List<Player> winners;
            winners = FindPlayersWithBiggestHandType(players);
            if (winners.Count > 1)
                winners = FindPlayersWithBiggestStrenght(winners);
            if (winners.Count > 1)
                winners = CompareEveryPlayerCard(winners);
            return winners;
        }
        private static List<Player> FindPlayersWithBiggestHandType(List<Player> players)
        {
            int biggestHandPower = players.Max(p => p.HandRank.Power);
            return players.Where(p => p.HandRank.Power == biggestHandPower).ToList();
        }
        private static List<Player> FindPlayersWithBiggestStrenght(List<Player> players)
        {
            int biggestStrenght = players.Max(p => p.HandRank.Strenght);
            return players.Where(p => p.HandRank.Strenght == biggestStrenght).ToList();
        }
        private static List<Player> CompareEveryPlayerCard(List<Player> players)
        {
            int biggestCard = 0;

            for (int i = 0; i < 5; i++)
            {
                biggestCard = 0;
                FilterCard(players, biggestCard, i);
                if (players.Count == 1)
                    return players;
            }
            return players;
        }
        private static void FilterCard(List<Player> players, int biggest, int card)
        {
            foreach (var player in players)
            {
                if (player.HandRank.Cards.Count == 0)
                    break;
                if (player.HandRank.Cards[card].Value > biggest)
                    biggest = player.HandRank.Cards[card].Value;
            }
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].HandRank.Cards.Count == 0)
                    break;
                if (players[i].HandRank.Cards[card].Value < biggest)
                {
                    players.Remove(players[i]);
                    i--;
                }
            }
        }
    }
}
