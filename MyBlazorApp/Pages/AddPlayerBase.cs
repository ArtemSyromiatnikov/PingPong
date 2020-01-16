using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Models;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Pages
{
    public class AddPlayerBase: ComponentBase
    {
        [Inject]
        public IPlayersService PlayersService { get; set; }
        
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        
        public AddPlayerViewModel Player { get; set; }
        
        protected override Task OnInitializedAsync()
        {
            Player = new AddPlayerViewModel();
            
            return base.OnInitializedAsync();
        }

        protected async Task SavePlayer()
        {
            var newPlayer = new PlayerStats
            {
                Id = 0,
                FirstName = Player.FirstName,
                LastName = Player.LastName,
                Wins = 0,
                Losses = 0 
            };

            var savedPlayer = await PlayersService.CreatePlayer(newPlayer);
            NavigationManager.NavigateTo("/players");

            Player = new AddPlayerViewModel();
        }
    }
}