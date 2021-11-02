using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiceCompare
{
    class Program
    {
        public static List<Player> Players { get; private set; }
        public static List<MatchUp> Matchups { get; private set; }

        static void Main(string[] args)
        {
             Players = SetPlayers(args);
             Matchups = CreateMatchups(Players);
            Matchups = PlayMatchups(Matchups);
            
            RankPlayers(Matchups);

        }

        private static void RankPlayers(List<MatchUp> matches)
        {
            foreach(var match in matches)
            {
                Console.WriteLine($"Match: {match.Player.Name} vs. {match.Oponent.Name} \n Winner:{match.Winner.Name}\n");
            }
            var victoris = new Dictionary<string, int>();
            foreach (var player in Players)
            {
                victoris.Add(player.Name, matches.Where(x=>x.Winner.Name==player.Name).Count());
                Console.WriteLine($"Player {player.Name} won {matches.Where(x => x.Winner.Name == player.Name).Count()} times");
            }
            Console.ReadLine();
        }

        private static List<MatchUp> PlayMatchups(List<MatchUp> matchups)
        {
            var results = new List<MatchUp>();
            foreach (var match in matchups)
                results.Add(PlayGames(match));
            return results;
        }

        private static MatchUp PlayGames(MatchUp match)
        {
           
            var winnerfound = false;
            while (!winnerfound)
            {
                match.PlayAnOtherGame();
                winnerfound = match.TryFindWinner(match);
            }
            return match;
        }

       
        private static List<MatchUp> CreateMatchups(List<Player> players)
        {
            var matchups = new List<MatchUp>();
            foreach (var player in players)
                foreach (var oponent in players)
                    if (player != oponent)
                        matchups.Add(new MatchUp(player, oponent));
            return matchups;
        }

        private static List<Player> SetPlayers(string[] args)
        {
            string filePath = "";
            if (args.Length == 0)
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
                    var lines = fileText.Split("\r\n");
                    var players = new List<Player>();
                    for (int i = 1; i < int.Parse(lines[0]); i++)
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
