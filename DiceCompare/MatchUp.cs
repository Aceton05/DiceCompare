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
        public Player Winner { get;  set; }
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
            var playercount = winner.Where(x => x.Name == match.Player.Name).Count();
            var oponentcount = winner.Where(x => x.Name == match.Oponent.Name).Count();
            switch (match.Games.Count)
            {
                case int n when (n < 4):
                    return false;
                case 4:
                    if (oponentcount == 0)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if (playercount == 0)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                case int n when (n < 10):
                    if ((playercount / oponentcount) > 4)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if ((oponentcount / playercount) > 4)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                case int n when (n < 40):
                    if ((playercount / oponentcount) > 2)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if ((oponentcount / playercount) > 2)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
                default:
                    if (playercount > oponentcount)
                    {
                        match.Winner = match.Player;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else if (oponentcount > playercount)
                    {
                        match.Winner = match.Oponent;
                        Console.WriteLine($"Player:{match.Winner.Name} won after {match.Games.Count} Games");
                        return true;
                    }
                    else
                        return false;
            }

        }
    }
}