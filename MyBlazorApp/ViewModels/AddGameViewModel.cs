using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.ViewModels
{
    public class AddGameViewModel
    {
        [Required(ErrorMessage = "Select Player 1")]
        public string Player1Id { get; set; }
        
        [Range(0, 100)]
        public int Player1Score { get; set; }
        
        [Required(ErrorMessage = "Select Player 2")]
        public string Player2Id { get; set; }
        
        [Range(0, 100)]
        public int Player2Score { get; set; }
    }
}