using System;
using System.Collections.Generic;

namespace DiceCompare
{
    internal class MatchUp
    {
        public MatchUp(Player player, Player oponent)
        {
            Player = player;
            Oponent = oponent;
        }

        public Player Player { get; private set; }
        public Player Oponent { get; private set; }
        public List<Game> Games { get; private set; }

        internal void PlayGame()
        {
            Game game = new Game(Player,Oponent);
            Games.Add(game);

        }
    }
}