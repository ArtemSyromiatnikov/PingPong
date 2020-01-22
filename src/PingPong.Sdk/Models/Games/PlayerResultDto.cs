namespace PingPong.Sdk.Models.Games
{
    public class PlayerResultDto
    {
        public PlayerDto PlayerDto { get; set; }
        public int       Score     { get; set; }
        public bool      IsWinner  { get; set; }
    }
}