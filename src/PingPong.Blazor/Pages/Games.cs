using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.Services;
using PingPong.Blazor.ViewModels;

namespace PingPong.Blazor.Pages
{
    public partial class Games
    {
        [Inject]
        private IGamesService GamesService { get; set; }
        
        private List<GameViewModel> GamesList { get; set; } = new List<GameViewModel>();
        private bool IsLoading { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            GamesList = await InitializeGames();
            IsLoading = false;

            await base.OnInitializedAsync();
        }

        private async Task<List<GameViewModel>> InitializeGames()
        {
            var games = await GamesService.GetGames();
            var viewModels = games.Select(g => new GameViewModel(g)).ToList();
            return viewModels;
        }
    }
}
