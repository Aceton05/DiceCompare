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
        public static string FilePath { get; private set; }
        /// <summary>
        /// Reades the given file to create games of "Mensch aerger dich nicht". The program determinates witch of the dices,
        /// given from the file, is the best to play this game. The program is part of the "40. Bundeswettbewerb Informatik"
        /// More infos about the task at:
        /// https://bwinf.de/fileadmin/bundeswettbewerb/40/Bundeswettbewerb-Aufgabenblatt.pdf
        /// You can find the repositorie at: https://github.com/Aceton05/DiceCompare        /// 
        /// </summary>
        /// <param name="args">Path of the file to work with</param>
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
            if (args.Length == 0)
            {
                Console.WriteLine("No File given. Please enter filepath");
                FilePath = Console.ReadLine();
            }
            else
                FilePath = args[0];
            var unreadable = true;
            while (unreadable)
            {
                try
                {
                    string fileText = File.ReadAllText(FilePath);
                    var lines = fileText.Split("\r\n");
                    var players = new List<Player>();
                    for (int i = 1; i <= int.Parse(lines[0]); i++)
                        players.Add(new Player(lines[i]));
                    return players;
                }
                catch
                {
                    Console.WriteLine("Can't use this File. Make sure it's formatet corekt. \n Please enter filepath");
                    FilePath = Console.ReadLine();
                }
            }
            return null;
        }
        /// <summary>
        /// Creates Matchups betwen all given player.
        /// Each player faces the other as host an as oponent.
        /// </summary>
        /// <param name="players"></param>
        /// <returns>List of MatchUps</returns>
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
        /// Plays the Game "Mensch aerger dich nicht" multible times and saves the result for the given MatchUps
        /// </summary>
        /// <param name="matchups">List of MatchUps to find a winner</param>
        /// <returns>Given list of MatchUp with winner set</returns>
        private static List<MatchUp> PlayMatchups(List<MatchUp> matchups)
        {
            var results = new List<MatchUp>();
            foreach (var match in matchups)
                results.Add(PlayGames(match));
            return results;
        }
        /// <summary>
        /// Sorts the Players based on the victories in the given MatchUps
        /// </summary>
        /// <param name="matches">List of played MatchUps</param>
        private static void RankPlayers(List<MatchUp> matches)
        {
            string resultText = $"\nTotal Players: {Players.Count}\nTotal Matches: {matches.Count}\n";
            var victoris = new Dictionary<Player, int>();
            foreach (var player in Players)
                player.Victoris = matches.Where(x => x.Winner.Name == player.Name).Count();
            Players = Players.OrderByDescending(x => x.Victoris).ToList();
            var i = 0;
            foreach (var p in Players)
            {
                i++;
                resultText += $"{i}. Place: Player {p.Name}\n Matches won: {p.Victoris} Games played: {p.GamesPlayed}\n";

            }
            Console.WriteLine(resultText);
            File.WriteAllText(FilePath.Replace(".txt", "_result.txt"), resultText);
            Console.WriteLine("Press any key to close this window . . .");
            Console.ReadKey();
        }

        /// <summary>
        /// Plays multible Games to find the winner of the MatchUp
        /// </summary>
        /// <param name="match">MatchUp to find a winner for</param>
        /// <returns>MatchUp with winner set</returns>
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
