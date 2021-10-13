namespace DiceCompare
{
    internal class MatchResult
    {
        public MatchResult(MatchUp match)
        {
            MatchUp = match;
        }

        public MatchUp MatchUp { get; private set; }

        public Player Winner { get; private set; }
    }
}