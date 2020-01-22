using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PingPong.API.Services;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService            _playersService;
        private readonly ILogger<PlayersController> _logger;

        public PlayersController(IPlayersService playersService, ILogger<PlayersController> logger)
        {
            _playersService = playersService;
            _logger         = logger;
        }

        [HttpGet]
        public async Task<Page<PlayerInfoDto>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var players = await _playersService.GetPlayers(page, pageSize);
            return players;
        }

        [HttpGet("/players/{id}")]
        public async Task<PlayerInfoDto> Get([FromRoute] int id)
        {
            var player = await _playersService.GetPlayerById(id);
            return player;
        }

        [HttpPost]
        public async Task<PlayerInfoDto> Create(CreatePlayerRequestDto request)
        {
            // TODO: Input validation
            var player = await _playersService.CreatePlayer(request);
            return player;
        }
    }
}