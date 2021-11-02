using System.Linq;

namespace DiceCompare
{
    public class Player
    {
        public Player(string line)
        {
            var splits = line.Split();
            Dice = new Dice(splits[0], splits.Skip(1).ToArray());
            Name = $"\"W{Dice.SideCount} [{string.Join(",", Dice.Sides)}]\"l";
        }

        public Dice Dice { get; private set; }
        public string Name { get; private set; }
    }
}