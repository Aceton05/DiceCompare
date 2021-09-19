using System;
using System.Collections.Generic;
using System.IO;

namespace DiceCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = SetPlayers(args);
            var matchups = CreateMatchups(players);
            var gameresults = PlayGames(matchups);
            var rnd = new Random();
            RankPlayers(gameresults);
            
        }

        private static void RankPlayers(List<GameResult> gameresults)
        {
            throw new NotImplementedException();
        }

        private static List<GameResult> PlayGames(List<MatchUp> matchups)
        {
            throw new NotImplementedException();
        }

        private static List<MatchUp> CreateMatchups(List<Player> players)
        {
            throw new NotImplementedException();
        }

        private static List<Player> SetPlayers(string[] args)
        {
            throw new NotImplementedException();            
        }
    }
}
