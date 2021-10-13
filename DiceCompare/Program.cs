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
            var matchResults = PlayMatchups(matchups);
            var rnd = new Random();
            RankPlayers(matchResults);

        }

        private static void RankPlayers(List<MatchResult> matchResults)
        {
            throw new NotImplementedException();
        }

        private static List<MatchResult> PlayMatchups(List<MatchUp> matchups)
        {
            var results = new List<MatchResult>();
            foreach (var match in matchups)
                results.Add(PlayGames(match));
            return results;
        }

        private static MatchResult PlayGames(MatchUp match)
        {
            var matchResult = new MatchResult(match);
            var nowinnerfound = true;
            while (nowinnerfound)
            {
                matchResult.MatchUp.PlayGame();
                nowinnerfound = TryFindWinner(matchResult);
            }
            return matchResult;
        }

        private static bool TryFindWinner(MatchResult matchResult)
        {
            throw new NotImplementedException("Logic still missing");
            switch (matchResult.MatchUp.Games.Count)
            {
                case int n when (n < 10):
                    return false;
                case int n when (n < 40):
                    return false;
                default:
                    return false;
            }

        }

        

        private static List<MatchUp> CreateMatchups(List<Player> players)
        {
            var matchups = new List<MatchUp>();
            foreach(var player in players)            
                foreach(var oponent in players)                
                    if (player != oponent)
                        matchups.Add(new MatchUp(player,oponent));
            return matchups;
        }

        private static List<Player> SetPlayers(string[] args)
        {
            string filePath = "";
            if (args == null)
            {
                Console.WriteLine("No File given. Please enter filepath");
                filePath = Console.ReadLine();
            }
            else
                filePath = args[0];
            string fileText = "";
            var unreadable = true;
            while (unreadable)
            {
                fileText = File.ReadAllText(filePath);
                try
                {
                    var lines = fileText.Split('\n');
                    var players = new List<Player>();
                    for (int i=1;i<int.Parse(lines[0]);i++)
                        players.Add(new Player(lines[i]));
                    return players;
                }
                catch
                {
                    Console.WriteLine("Can't use this File. Make sure it's formatet corekt. \n Please enter filepath");
                    filePath = Console.ReadLine();
                }
            }
            return null;
        }
    }
}
