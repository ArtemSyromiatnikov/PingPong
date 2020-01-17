using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Pages
{
    public partial class Players
    {
        [Inject]
        private IPlayersService PlayersService { get; set; }

        private List<PlayerViewModel> PlayersList { get; set; } = new List<PlayerViewModel>();
        private bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            PlayersList = await InitializePlayers();
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