using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp
{
    public interface IPlayersService
    {
        Task<List<PlayerStats>> GetPlayerStats();
        Task<PlayerStats> CreatePlayer(PlayerStats player);
    }
}