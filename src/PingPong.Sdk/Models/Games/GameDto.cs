using System;

namespace PingPong.Sdk.Models.Games
{
    public class GameDto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public PlayerResultDto Player1Result { get; set; }
        public PlayerResultDto Player2Result { get; set; }
    }
}
