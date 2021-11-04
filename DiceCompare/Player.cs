using System;
using System.Collections.Generic;
using System.Linq;

namespace DiceCompare
{
    public class Player
    {
        public Player(string line)
        {
            var splits = line.Split();
            Dice = new Dice(splits[0], splits.Skip(1).ToArray());
            Name = $"\"W{Dice.SideCount} [{string.Join(",", Dice.Sides)}]\"";
        }
        public Player(string name,bool onlyName)
        {
            Name = name;
        }

        public Dice Dice { get; private set; }
        public string Name { get; private set; }
        public int Startfield { get; internal set; }
        public int GoalEntrance { get; internal set; }
        public List<int?> Figures { get; internal set; }
        public int NoMoveCount { get; internal set; }

        internal List<int?> GetFigurePositions()
        {
            return Figures;
        }
    }
}