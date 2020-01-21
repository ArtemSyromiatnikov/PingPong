using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(ILogger<PlayersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Page<PlayerInfo> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return new Page<PlayerInfo>(18,
                new List<PlayerInfo>
                {
                    new PlayerInfo()
                        {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5},
                    new PlayerInfo()
                        {Id = 2, FirstName = "Martin", LastName = "Stewart", Wins = 9, Losses = 1},
                    new PlayerInfo()
                        {Id = 3, FirstName = "Professor", LastName = "McGonagall", Wins = 6, Losses = 5},
                    new PlayerInfo()
                        {Id = 4, FirstName = "Professor", LastName = "Flitwick", Wins = 0, Losses = 0},
                }
            );
        }
        
        [HttpGet("/players/{id}")]
        public PlayerInfo Get([FromRoute] int id)
        {
            return new PlayerInfo() {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5};
        }

        [HttpPost]
        public PlayerInfo Create(CreatePlayerRequest request)
        {
            var player = new PlayerInfo
            {
                Id        = 7,
                FirstName = request.FirstName,
                LastName  = request.LastName,
                Wins      = 0,
                Losses    = 0
            };

            return player;
        }
    }
}