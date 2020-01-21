using System;

namespace PingPong.Sdk.Models.Games
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public PlayerResult Player1Result { get; set; }
        public PlayerResult Player2Result { get; set; }
    }

    public class PlayerResult
    {
        public Player Player { get; set; }
        public int Score { get; set; }
        public bool IsWinner { get; set; }
    }
    
    public class Player
    {
        public int    Id        { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
    }
}
