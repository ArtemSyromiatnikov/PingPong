using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPong.API.Services;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGamesService gamesService, ILogger<GamesController> logger)
        {
            _gamesService = gamesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Page<Game>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var games = await _gamesService.GetGames(page, pageSize);
            return games;
        }

        [HttpPost]
        public async Task<Game> Create(CreateGameRequest request)
        {
            var game = await _gamesService.CreateGame(request);
            return game;
        }
    }
}