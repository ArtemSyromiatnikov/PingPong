using System.Threading.Tasks;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;

namespace PingPong.Sdk.Resources
{
    public interface IGamesResource
    {
        Task<Page<GameDto>> GetGames(int page = 1, int pageSize = 10);
        Task<GameDto>       GetGame(int id);
        Task<GameDto>       CreateGame(CreateGameRequestDto game);
    }
}