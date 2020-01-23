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

        private bool IsLoading  { get; set; } = true;
        public  int  Page       { get; set; } = 1;
        public  int  PageSize   { get; set; } = 10;
        public  int  TotalItems { get; set; } = 0;

        private List<GameViewModel> GamesList { get; set; } = new List<GameViewModel>();

        protected override async Task OnInitializedAsync()
        {
            await InitializeGames();

            await base.OnInitializedAsync();
        }

        private async Task InitializeGames()
        {
            IsLoading = true;

            var games = await ApiClient.Games.GetGames(Page, PageSize);
            GamesList  = games.Items.Select(g => new GameViewModel(g)).ToList();
            TotalItems = games.TotalItems;

            IsLoading = false;
        }

        private async Task HandlePageChanged(int page)
        {
            Page = page;
            await InitializeGames();
        }
    }
}