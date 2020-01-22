using System.Threading.Tasks;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.Sdk.Resources
{
    public interface IPlayersResource
    {
        Task<Page<PlayerInfoDto>> GetPlayers(int page, int pageSize);
        Task<PlayerInfoDto>       GetPlayerById(int playerId);
        Task<PlayerInfoDto>       CreatePlayer(CreatePlayerRequestDto player);
    }
}