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

        private bool IsLoading  { get; set; } = true;
        public  int  Page       { get; set; } = 1;
        public  int  PageSize   { get; set; } = 10;
        public  int  TotalItems { get; set; } = 0;

        private List<PlayerViewModel> PlayersList { get; set; } = new List<PlayerViewModel>();


        protected override async Task OnInitializedAsync()
        {
            await FetchPlayers();

            await base.OnInitializedAsync();
        }

        private async Task FetchPlayers()
        {
            IsLoading = true;
            var playersPage = await ApiClient.Players.GetPlayers(Page, PageSize);

            PlayersList = playersPage.Items.Select(p => new PlayerViewModel(p)).ToList();
            TotalItems  = playersPage.TotalItems;

            IsLoading = false;
        }

        private async Task HandlePageChanged(int page)
        {
            Page = page;
            await FetchPlayers();
        }
    }
}