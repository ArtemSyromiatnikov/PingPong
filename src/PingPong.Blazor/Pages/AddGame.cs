using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;
using PingPong.Sdk.Models.Games;

namespace PingPong.Blazor.Pages
{
    public partial class AddGame
    {
        [Inject] private IApiClient ApiClient { get; set; }


        [Inject] private NavigationManager NavigationManager { get; set; }

        private bool                  IsLoading { get; set; } = true;
        private List<PlayerViewModel> Players   { get; set; } = new List<PlayerViewModel>();
        private AddGameViewModel      Game      { get; set; } = new AddGameViewModel();
        private bool                  IsSaving  { get; set; } = false;

        private EditContext EditContext { get; set; }


        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(Game);

            IsLoading = true;

            var playersDto = await ApiClient.Players.GetPlayers(1, 1000);
            Players        = playersDto.Items.Select(p => new PlayerViewModel(p)).ToList();
            Game.Player1Id = Players.FirstOrDefault()?.Id.ToString();
            Game.Player2Id = Players.FirstOrDefault()?.Id.ToString();

            // When Player 1 changes, force Player 2 revalidation
            Game.OnPlayer1Changed += (sender, s) =>
            {
                var player2Field = new FieldIdentifier(Game, nameof(Game.Player2Id));
                EditContext.NotifyFieldChanged(player2Field);
            };

            // When Player 1 Score changes, force Player 2 Score revalidation
            Game.OnPlayer1ScoreChanged += (sender, s) =>
            {
                var player2ScoreField = new FieldIdentifier(Game, nameof(Game.Player2Score));
                EditContext.NotifyFieldChanged(player2ScoreField);
            };

            IsLoading = false;
        }


        private async Task SaveGame()
        {
            var newGameDto = new CreateGameRequestDto
            {
                Player1Id    = int.Parse(Game.Player1Id),
                Player2Id    = int.Parse(Game.Player2Id),
                Player1Score = Game.Player1Score,
                Player2Score = Game.Player2Score
            };

            IsSaving = true;
            await ApiClient.Games.CreateGame(newGameDto);

            NavigationManager.NavigateTo("/games");
        }
    }
}