using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Models;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Pages
{
    public class PlayersBase: ComponentBase
    {
        protected List<PlayerViewModel> Players { get; set; }

        protected override Task OnInitializedAsync()
        {
            Players = InitializePlayers();
            return base.OnInitializedAsync();
        }

        private List<PlayerViewModel> InitializePlayers()
        {
            var playerModels = DummyPlayers.Select(p => new PlayerViewModel(p)).ToList();
            return playerModels;
        }

        private List<PlayerStats> DummyPlayers =>
            new List<PlayerStats>
            {
                new PlayerStats() { Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5 },
                new PlayerStats() { Id = 2, FirstName = "Martin", LastName = "Stewart", Wins = 9, Losses = 1 },
                new PlayerStats() { Id = 3, FirstName = "Professor", LastName = "McGonagall", Wins = 6, Losses = 5 },
                new PlayerStats() { Id = 3, FirstName = "Professor", LastName = "Flitwick", Wins = 0, Losses = 0 },
            };
    }
}