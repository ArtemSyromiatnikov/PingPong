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
            
            Player1Result = new PlayerResultViewModel(game.Player1Result);
            Player2Result = new PlayerResultViewModel(game.Player2Result);
        }
    }

    public class PlayerResultViewModel
    {
        public PlayerResultViewModel(PlayerResult playerResult)
        {
            PlayerId = playerResult.Player.Id;
            FullName = $"{playerResult.Player.FirstName} {playerResult.Player.LastName}".Trim();
            Score    = playerResult.Score;
            IsWinner = playerResult.IsWinner;
        }

        public int PlayerId { get; }
        public string FullName { get; }
        public int Score { get; }
        public bool IsWinner { get; }
    }
}
