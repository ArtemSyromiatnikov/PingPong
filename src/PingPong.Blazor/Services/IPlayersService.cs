using System.Collections.Generic;
using System.Threading.Tasks;
using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerInfo>> GetPlayers();
        Task<PlayerInfo> GetPlayerById(int playerId);
        Task<PlayerInfo> CreatePlayer(CreatePlayerRequest player);
    }
}