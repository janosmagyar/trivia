namespace Trivia
{
    public class Player
    {
        public string Name { get;  }
        public PlayerType PlayerType { get; }
        public int Purse { get; set; }
        public bool IsInPenaltyBox { get; set; }

        public Player(string name, PlayerType playerType = PlayerType.Adult)
        {
            Name = name;
            PlayerType = playerType;
        }
    }
}