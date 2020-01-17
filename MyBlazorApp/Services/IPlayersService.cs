using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public interface IPlayersService
    {
        Task<List<PlayerStats>> GetPlayers();
        Task<PlayerStats> GetPlayerById(int playerId);
        Task<PlayerStats> CreatePlayer(PlayerStats player);
    }
}