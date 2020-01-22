namespace PingPong.Sdk.Models.Games
{
    public class CreateGameRequestDto
    {
        public int Player1Id { get; set; }
        
        public int Player1Score { get; set; }
        
        public int Player2Id { get; set; }
        
        public int Player2Score { get; set; }
    }
}