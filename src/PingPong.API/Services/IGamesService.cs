using System.Collections.Generic;
using System.Threading.Tasks;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;

namespace PingPong.API.Services
{
    public interface IGamesService
    {
        Task<Page<Game>> GetGames(int page = 1, int pageSize = 10);
        Task<Game> GetGame(int id);
        Task<Game> CreateGame(CreateGameRequest request);
    }
}