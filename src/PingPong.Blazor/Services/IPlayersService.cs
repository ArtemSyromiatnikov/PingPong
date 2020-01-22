using System.Collections.Generic;
using System.Threading.Tasks;
using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerInfoDto>> GetPlayers();
        Task<PlayerInfoDto> GetPlayerById(int playerId);
        Task<PlayerInfoDto> CreatePlayer(CreatePlayerRequestDto player);
    }
}