namespace Trivia
{
    public class Player
    {
        public string Name { get;  }
        public int Purse { get; set; }
        public int Place { get; set; }
        public bool IsInPenaltyBox { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}