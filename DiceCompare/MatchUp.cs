using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceCompare
{
    public class MatchUp
    {
        public MatchUp(Player player, Player oponent)
        {
            Player = player;
            Oponent = oponent;
            Games = new List<Game>();
        }

        public Player Player { get; private set; }
        public Player Oponent { get; private set; }
        public Player Winner { get; set; }
        public List<Game> Games { get; private set; }

        internal void PlayAnOtherGame()
        {
            Game game = new Game(Player, Oponent);
            game.Play();
            Games.Add(game);
        }

        public bool TryFindWinner(MatchUp match)
        {
            var winner = match.Games.Select(game => game.Winner).ToList();
            var playerCount = winner.Where(x => x.Name == match.Player.Name).Count();
            var oponentCount = winner.Where(x => x.Name == match.Oponent.Name).Count();
            var noOneCount = winner.Where(x => x.Name == "No Winner").Count();
            if(noOneCount>10)
            {
                match.Winner = new Player("No Winner", true);
                throw new Exception($"There Cant be a winner in this Match {match.Player.Name} vs. {match.Oponent.Name}");
            }
            switch (match.Games.Count)
            {
                case int n when (n < 5):
                    return false;
                case 5:
                    if (playerCount == 5)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if (oponentCount == 5)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                case int n when (n < 11):
                    if ((playerCount / (oponentCount + noOneCount)) > 4)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if ((oponentCount / (playerCount + noOneCount)) > 4)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                case int n when (n <= 40):
                    if ((playerCount / (oponentCount + noOneCount)) > 2)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if ((oponentCount / (playerCount + noOneCount)) > 2)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                default:
                    if (playerCount > oponentCount)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if (oponentCount > playerCount)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                    {
                        match.Winner = new Player("No Winner", true);
                        throw new Exception($"There Cant be a clear in this Match {match.Player.Name} vs. {match.Oponent.Name}");
                    }
            }

        }
    }
}