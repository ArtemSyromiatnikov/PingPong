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
        [Inject]
        private IPlayersService PlayersService { get; set; }
        
        protected List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
        protected bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            Players = await InitializePlayers();
            IsLoading = false;

            await base.OnInitializedAsync();
        }

        private async Task<List<PlayerViewModel>> InitializePlayers()
        {
            var players = await PlayersService.GetPlayerStats();
            var playerModels = players.Select(p => new PlayerViewModel(p)).ToList();
            return playerModels;
        }
    }
}