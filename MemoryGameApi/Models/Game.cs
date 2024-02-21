namespace MemoryGameApi.Models
{
    public class Game
    {
        public List<Card> Cards { get; set; } = new();
        public int Player1Score { get; set; } = 0;
        public int Player2Score { get; set; } = 0;
        public int CurrentPlayer { get; set; } = 1; 
        public bool IsGameOver { get; set; } = false;
    }

    public class Card
    {
        public string Value { get; set; }
        public bool IsMatched { get; set; } = false;
        public bool IsFlipped { get; set; } = false;
    }
}
