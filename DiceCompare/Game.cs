using System;

namespace DiceCompare
{
    internal class Game
    {
        private Player player;
        private Player oponent;
        private Player winner;

        public Game(Player player, Player oponent)
        {
            this.player = player;
            this.oponent = oponent;
        }

        internal void Play()
        {
            winner = player;
            Console.WriteLine($"winner: {winner.Name}");
        }
    }
}