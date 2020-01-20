using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerInfo>> GetPlayers();
        Task<PlayerInfo> GetPlayerById(int playerId);
        Task<PlayerInfo> CreatePlayer(CreatePlayerRequest player);
    }
}