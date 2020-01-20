using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Models;
using MyBlazorApp.Services;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Pages
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