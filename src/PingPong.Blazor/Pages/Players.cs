using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.Utils;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;

namespace PingPong.Blazor.Pages
{
    public partial class Players
    {
        [Inject] private IApiClient        ApiClient         { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        private bool                  IsLoading   { get; set; } = true;
        public  int                   Page        { get; set; }
        public  int                   PageSize    { get; set; }
        public  int                   TotalItems  { get; set; }
        private List<PlayerViewModel> PlayersList { get; set; } = new List<PlayerViewModel>();


        protected override async Task OnInitializedAsync()
        {
            PageSize = NavigationManager.ReadQueryStringAsInt("pageSize", 10);
            Page     = NavigationManager.ReadQueryStringAsInt("page", 1);

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