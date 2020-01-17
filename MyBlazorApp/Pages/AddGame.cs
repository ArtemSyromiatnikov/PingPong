using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorApp.Models;
using MyBlazorApp.Services;
using MyBlazorApp.ViewModels;

namespace MyBlazorApp.Pages
{
    public partial class AddGame
    {
        [Inject]
        private IGamesService GamesService { get; set; }
        [Inject]
        private IPlayersService PlayersService { get; set; }
        
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private bool IsLoading { get; set; } = true;
        private List<PlayerViewModel> Players { get; set; } = new List<PlayerViewModel>();
        private AddGameViewModel Game { get; set; } = new AddGameViewModel();
        private bool IsSaving { get; set; } = false;
        

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            
            var playersDto = await PlayersService.GetPlayers();
            Players = playersDto.Select(p => new PlayerViewModel(p)).ToList();
            Game.Player1Id = Players.FirstOrDefault()?.Id.ToString();
            Game.Player2Id = Players.FirstOrDefault()?.Id.ToString();
            
            IsLoading = false;
        }

        private async Task SaveGame()
        {
            // TODO: Players should be different...
            // TODO: Score validation...

            var newGameDto = new CreateGameRequest
            {
                Player1Id    = int.Parse(Game.Player1Id),
                Player2Id    = int.Parse(Game.Player2Id),
                Player1Score = Game.Player1Score,
                Player2Score = Game.Player2Score
            };
            
            IsSaving = true;
            var savedGame = await GamesService.CreateGame(newGameDto);
            NavigationManager.NavigateTo("/games");
        }
    }
}