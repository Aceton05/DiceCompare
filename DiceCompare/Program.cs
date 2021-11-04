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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Players = SetPlayers(args);
            Matchups = CreateMatchups(Players);
            Matchups = PlayMatchups(Matchups);

            RankPlayers(Matchups);

        }
        /// <summary>
        /// Creates Player from the given File
        /// </summary>
        /// <param name="args">First Program arg should be the filepath</param>
        /// <returns>List of Players</returns>
        private static List<Player> SetPlayers(string[] args)
        {
            string filePath;
            if (args.Length == 0)
            {
                Console.WriteLine("No File given. Please enter filepath");
                filePath = Console.ReadLine();
            }
            else
                filePath = args[0];
            var unreadable = true;
            while (unreadable)
            {
                string fileText = File.ReadAllText(filePath);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        private static List<MatchUp> CreateMatchups(List<Player> players)
        {
            var matchups = new List<MatchUp>();
            foreach (var player in players)
                foreach (var oponent in players)
                    if (player != oponent)
                        matchups.Add(new MatchUp(player, oponent));
            return matchups;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchups"></param>
        /// <returns></returns>
        private static List<MatchUp> PlayMatchups(List<MatchUp> matchups)
        {
            var results = new List<MatchUp>();
            foreach (var match in matchups)
                results.Add(PlayGames(match));
            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matches"></param>
        private static void RankPlayers(List<MatchUp> matches)
        {            
            Console.WriteLine($"\nTotal Matches: {matches.Count}\n");
            var victoris = new Dictionary<string, int>();
            foreach (var player in Players)
                victoris.Add(player.Name, matches.Where(x => x.Winner.Name == player.Name).Count());
            victoris = victoris.OrderByDescending(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
            foreach (var v in victoris)
                Console.WriteLine($"Player {v.Key} won {v.Value} matches");
            Console.ReadLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static MatchUp PlayGames(MatchUp match)
        {
            Console.WriteLine($"-----------------\n{match.Player.Name} vs. {match.Oponent.Name}\n");
            try
            {
                var winnerfound = false;
                while (!winnerfound)
                {
                    match.PlayAnOtherGame();
                    winnerfound = match.TryFindWinner(match);                    
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }     
            return match;
        }
    }
}
