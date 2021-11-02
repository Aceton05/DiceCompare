using System;

namespace DiceCompare
{
    public class Dice
    {
        public Dice(string v, string[] vs)
        {
            SideCount = int.Parse(v);
            Sides = new int[SideCount];
            for (int i = 0; i < vs.Length; i++)
                Sides[i] = int.Parse(vs[i]);

        }

        public int SideCount { get; private set; }
        public int[] Sides { get; private set; }

        public int Role()
        {
            return Sides[new Random().Next(0, SideCount)];
        }
    }
}