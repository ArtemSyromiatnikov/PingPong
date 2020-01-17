namespace MyBlazorApp.Models
{
    public class CreateGameRequest
    {
        public int Player1Id { get; set; }
        
        public int Player1Score { get; set; }
        
        public int Player2Id { get; set; }
        
        public int Player2Score { get; set; }
    }
}