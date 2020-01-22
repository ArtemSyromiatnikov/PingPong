using System.Collections.Generic;
using System.Threading.Tasks;
using PingPong.Sdk.Models.Games;

namespace PingPong.Blazor.Services
{
    public interface IGamesService
    {
        Task<List<GameDto>> GetGames();
        Task<GameDto> CreateGame(CreateGameRequestDto newGameRequest);
    }
}