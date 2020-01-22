using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;

namespace PingPong.Blazor.Pages
{
    public partial class Players
    {
        [Inject] private IApiClient ApiClient { get; set; }

        private List<PlayerViewModel> PlayersList { get; set; } = new List<PlayerViewModel>();
        private bool                  IsLoading   { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            IsLoading   = true;
            PlayersList = await InitializePlayers();
            IsLoading   = false;

            await base.OnInitializedAsync();
        }

        private async Task<List<PlayerViewModel>> InitializePlayers()
        {
            var playersPage = await ApiClient.Players.GetPlayers(1, 1000);

            var playerModels = playersPage.Items.Select(p => new PlayerViewModel(p)).ToList();
            return playerModels;
        }
    }
}