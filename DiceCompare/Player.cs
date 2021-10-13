namespace DiceCompare
{
    internal class Player
    {
        public Player(string line)
        {
            var splits = line.Split();
            Dice = new int[int.Parse(splits[0])];
            for (int i = 1; i < splits.Length; i++)
                Dice[i - 1] = int.Parse(splits[i]);
            Name = $"W{Dice.Length} [{string.Join(",",Dice)}]";
        }

        public int[] Dice { get; private set; }
        public string Name { get; private set; }
    }
}