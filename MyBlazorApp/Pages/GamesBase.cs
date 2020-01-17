using Microsoft.AspNetCore.Components;
using MyBlazorApp.Models;
using MyBlazorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlazorApp.Pages
{
    public class GamesBase : ComponentBase
    {
        protected List<GameViewModel> Games { get; set; }

        protected override Task OnInitializedAsync()
        {
            Games = InitializeGames();

            return base.OnInitializedAsync();
        }

        private List<GameViewModel> InitializeGames()
        {
            var games = RetrieveGames();
            var viewModels = games.Select(g => new GameViewModel(g)).ToList();
            return viewModels;
        }

        /// <summary>
        /// Imitates data retrieved from API
        /// </summary>
        /// <returns></returns>
        private List<Game> RetrieveGames()
        {
            return new List<Game>() { 
                new Game
                {
                    Id = 1,
                    Timestamp = new DateTime(2020, 1, 15, 8, 0, 0),
                    Player1 = new Player { Id = 1, FirstName = "Artem", LastName = "Syromiatnikov" },
                    Player2 = new Player { Id = 2, FirstName = "Martin", LastName = "Stewart" },
                    Player1Score = 8,
                    Player2Score = 10
                },
                new Game
                {
                    Id = 2,
                    Timestamp = new DateTime(2020, 1, 15, 14, 0, 0),
                    Player1 = new Player { Id = 1, FirstName = "Artem", LastName = "Syromiatnikov" },
                    Player2 = new Player { Id = 2, FirstName = "Martin", LastName = "Stewart" },
                    Player1Score = 2,
                    Player2Score = 10
                },
                new Game
                {
                    Id = 3,
                    Timestamp = new DateTime(2020, 1, 15, 14, 15, 0),
                    Player1 = new Player { Id = 1, FirstName = "Artem", LastName = "Syromiatnikov" },
                    Player2 = new Player { Id = 2, FirstName = "Martin", LastName = "Stewart" },
                    Player1Score = 10,
                    Player2Score = 3
                }
            };
        }
    }
}
