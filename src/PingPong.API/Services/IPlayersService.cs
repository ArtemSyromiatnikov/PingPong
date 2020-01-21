using System.Threading.Tasks;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Services
{
    public interface IPlayersService
    {
        Task<Page<PlayerInfo>> GetPlayers(int page, int pageSize);
        Task<PlayerInfo> GetPlayerById(int playerId);
        Task<PlayerInfo> CreatePlayer(CreatePlayerRequest player);
    }
}