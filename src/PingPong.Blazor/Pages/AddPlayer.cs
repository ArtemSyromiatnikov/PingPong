using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.Models;
using PingPong.Blazor.Services;
using PingPong.Blazor.ViewModels;

namespace PingPong.Blazor.Pages
{
    public partial class AddPlayer
    {
        [Inject]
        private IPlayersService PlayersService { get; set; }
        
        [Inject]
        private NavigationManager NavigationManager { get; set; }


        private AddPlayerViewModel Player { get; set; } = new AddPlayerViewModel();
        private bool IsSaving { get; set; } = false;
        
        protected async Task SavePlayer()
        {
            var newPlayer = new CreatePlayerRequest()
            {
                FirstName = Player.FirstName,
                LastName = Player.LastName,
            };

            IsSaving = true;
            var savedPlayer = await PlayersService.CreatePlayer(newPlayer);
            NavigationManager.NavigateTo("/players");
        }
    }
}