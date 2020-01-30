using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PingPong.Blazor.Validators;
using PingPong.Blazor.ViewModels;
using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.Pages
{
    public partial class AddPlayer
    {
        [Inject] private IApiClient ApiClient { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }


        private ServerSideValidator ServerSideValidator { get; set; }
        private AddPlayerViewModel  Player              { get; set; } = new AddPlayerViewModel();
        private bool                IsSaving            { get; set; } = false;
        private bool                HasFailed           { get; set; } = false;
        private string              ErrorMessage        { get; set; } = String.Empty;
        
        protected async Task SavePlayer()
        {
            IsSaving     = true;
            HasFailed    = false;
            ErrorMessage = String.Empty;
            
            var newPlayer = new CreatePlayerRequestDto()
            {
                FirstName = Player.FirstName,
                LastName  = Player.LastName,
            };

            try
            {
                await ApiClient.Players.CreatePlayer(newPlayer);

                NavigationManager.NavigateTo("/players");
            }
            catch (ApiException ex)
            {
                if (ex.Error.IsValidationException)
                {
                    ServerSideValidator.DisplayErrors(ex.Error.ValidationErrors);
                }
                else
                {
                    HasFailed    = true;
                    ErrorMessage = ex.Error.Message;
                }
            }
            finally
            {
                IsSaving = false;
            }
        }
    }
}