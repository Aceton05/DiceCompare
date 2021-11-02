using System;

namespace DiceCompare
{
    public class Game
    {
        private Player player;
        private Player oponent;
        private Player winner;

        public Game(Player player, Player oponent)
        {
            this.player = player;
            this.oponent = oponent;
        }

        public Player Winner { get => winner; set => winner = value; }

        public void Play()
        {
            if (player.Dice.Role() > oponent.Dice.Role())
                winner = player;
            else
                winner = oponent;
        }
    }
}