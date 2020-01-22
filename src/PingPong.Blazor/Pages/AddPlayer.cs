using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.Pages
{
    public partial class AddPlayer
    {
        [Inject] private IApiClient ApiClient { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }


        private AddPlayerViewModel Player   { get; set; } = new AddPlayerViewModel();
        private bool               IsSaving { get; set; } = false;

        protected async Task SavePlayer()
        {
            var newPlayer = new CreatePlayerRequestDto()
            {
                FirstName = Player.FirstName,
                LastName  = Player.LastName,
            };

            IsSaving = true;
            await ApiClient.Players.CreatePlayer(newPlayer);

            NavigationManager.NavigateTo("/players");
        }
    }
}