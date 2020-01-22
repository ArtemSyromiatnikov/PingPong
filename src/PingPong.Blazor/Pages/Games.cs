using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;

namespace PingPong.Blazor.Pages
{
    public partial class Games
    {
        [Inject] private IApiClient ApiClient { get; set; }

        private List<GameViewModel> GamesList { get; set; } = new List<GameViewModel>();
        private bool                IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            GamesList = await InitializeGames();
            IsLoading = false;

            await base.OnInitializedAsync();
        }

        private async Task<List<GameViewModel>> InitializeGames()
        {
            var games = await ApiClient.Games.GetGames(1, 1000);
            var viewModels = games.Items.Select(g => new GameViewModel(g)).ToList();
            return viewModels;
        }
    }
}