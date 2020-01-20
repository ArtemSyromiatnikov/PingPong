using System;

namespace MyBlazorApp.ViewModels
{
    public class AddGameViewModel
    {
        public event EventHandler<string> OnPlayer1Changed;
        public event EventHandler<int> OnPlayer1ScoreChanged;
        
        
        private string _player1Id;
        public string Player1Id
        {
            get => _player1Id;
            set
            {
                _player1Id = value;
                OnPlayer1Changed?.Invoke(this, _player1Id);
            }
        }

        private int _player1Score;
        public int Player1Score
        {
            get => _player1Score;
            set
            {
                _player1Score = value;
                OnPlayer1ScoreChanged?.Invoke(this, _player1Score);
            }
        }

        public string Player2Id { get; set; }
        
        public int Player2Score { get; set; }
    }
}