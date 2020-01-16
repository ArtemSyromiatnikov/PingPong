using MyBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlazorApp.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public PlayerResultViewModel Player1Result { get; set; }
        public PlayerResultViewModel Player2Result { get; set; }

        public GameViewModel(Game game)
        {
            Id = game.Id;
            Timestamp = game.Timestamp;
            
            bool isPlayer1Winner = game.Player1Score > game.Player2Score;
            Player1Result = new PlayerResultViewModel(game.Player1, game.Player1Score, isPlayer1Winner);
            Player2Result = new PlayerResultViewModel(game.Player2, game.Player2Score, !isPlayer1Winner);
        }
    }

    public class PlayerResultViewModel
    {
        public PlayerResultViewModel(Player player, int score, bool isWinner)
        {
            PlayerId = player.Id;
            FullName = $"{player.FirstName} {player.LastName}".Trim();
            Score = score;
            IsWinner = isWinner;
        }

        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }
        public bool IsWinner { get; set; }
    }
}
