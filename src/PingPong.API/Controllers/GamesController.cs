using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;

        public GamesController(ILogger<GamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Page<Game> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return new Page<Game>(44, new List<Game>()
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
            });
        }

        [HttpPost]
        public Game Create(CreateGameRequest request)
        {
            var player1 = new PlayerInfo()
                {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5};
            var player2 = new PlayerInfo()
                {Id = 2, FirstName = "Martin", LastName = "Stewart", Wins = 9, Losses = 1};
            
            var game = new Game()
            {
                Id        = 14,
                Timestamp = DateTime.Now,
                Player1Result = new PlayerResult()
                {
                    Player = new Player
                    {
                        Id        = player1.Id,
                        FirstName = player1.FirstName,
                        LastName  = player1.LastName
                    },
                    Score    = request.Player1Score,
                    IsWinner = request.Player1Score > request.Player2Score
                },
                Player2Result = new PlayerResult()
                {
                    Player = new Player
                    {
                        Id        = player2.Id,
                        FirstName = player2.FirstName,
                        LastName  = player2.LastName
                    },
                    Score    = request.Player2Score,
                    IsWinner = request.Player2Score > request.Player1Score
                }
            };

            return game;
        }
    }
}