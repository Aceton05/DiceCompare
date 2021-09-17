using System;
using System.Collections.Generic;
using System.IO;

namespace DiceCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = SetPlayers(args);
            List<MatchUps> matchups = CreateMatchups(players);
            List<GameResults> gameresults = PlayGames(matchups);
            RankPlayers(gameresults);
            
        }

        private static void RankPlayers(List<GameResults> gameresults)
        {
            throw new NotImplementedException();
        }

        private static List<GameResults> PlayGames(List<MatchUps> matchups)
        {
            throw new NotImplementedException();
        }

        private static List<MatchUps> CreateMatchups(List<Player> players)
        {
            throw new NotImplementedException();
        }

        private static List<Player> SetPlayers(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
