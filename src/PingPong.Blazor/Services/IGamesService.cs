using System.Collections.Generic;
using System.Threading.Tasks;
using PingPong.Blazor.Models;

namespace PingPong.Blazor.Services
{
    public interface IGamesService
    {
        Task<List<Game>> GetGames();
        Task<Game> CreateGame(CreateGameRequest newGameRequest);
    }
}