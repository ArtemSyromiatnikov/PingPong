using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public class GamesService : IGamesService
    {
        private readonly IPlayersService _playersService;

        public GamesService(IPlayersService playersService)
        {
            _playersService = playersService;
        }

        public List<Game> Games = new List<Game>()
        {
            new Game
            {
                Id        = 1,
                Timestamp = new DateTime(2020, 1, 15, 8, 0, 0),
                Player1Result = new PlayerResult()
                {
                    Player   = new Player {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov"},
                    Score    = 8,
                    IsWinner = false
                },
                Player2Result = new PlayerResult()
                {
                    Player   = new Player {Id = 2, FirstName = "Martin", LastName = "Stewart"},
                    Score    = 11,
                    IsWinner = true
                }
            },
            new Game
            {
                Id        = 2,
                Timestamp = new DateTime(2020, 1, 15, 14, 0, 0),
                Player1Result = new PlayerResult()
                {
                    Player   = new Player {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov"},
                    Score    = 2,
                    IsWinner = false
                },
                Player2Result = new PlayerResult()
                {
                    Player   = new Player {Id = 2, FirstName = "Martin", LastName = "Stewart"},
                    Score    = 11,
                    IsWinner = true
                }
            },
            new Game
            {
                Id        = 3,
                Timestamp = new DateTime(2020, 1, 15, 14, 15, 0),
                Player1Result = new PlayerResult()
                {
                    Player   = new Player {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov"},
                    Score    = 11,
                    IsWinner = true
                },
                Player2Result = new PlayerResult()
                {
                    Player   = new Player {Id = 2, FirstName = "Martin", LastName = "Stewart"},
                    Score    = 3,
                    IsWinner = false
                }
            }
        };

        public async Task<List<Game>> GetGames()
        {
            await Task.Delay(1000);

            return Games;
        }

        public async Task<Game> CreateGame(CreateGameRequest newGameRequest)
        {
            await Task.Delay(1000);

            var player1 = await _playersService.GetPlayerById(newGameRequest.Player1Id);
            var player2 = await _playersService.GetPlayerById(newGameRequest.Player2Id);

            var maxId = Games.Max(p => p.Id);
            var game = new Game()
            {
                Id        = maxId + 1,
                Timestamp = DateTime.Now,
                Player1Result = new PlayerResult()
                {
                    Player = new Player
                    {
                        Id        = player1.Id,
                        FirstName = player1.FirstName,
                        LastName  = player1.LastName
                    },
                    Score    = newGameRequest.Player1Score,
                    IsWinner = newGameRequest.Player1Score > newGameRequest.Player2Score
                },
                Player2Result = new PlayerResult()
                {
                    Player = new Player
                    {
                        Id        = player2.Id,
                        FirstName = player2.FirstName,
                        LastName  = player2.LastName
                    },
                    Score    = newGameRequest.Player2Score,
                    IsWinner = newGameRequest.Player2Score > newGameRequest.Player1Score
                }
            };

            Games.Add(game);

            return game;
        }
    }
}