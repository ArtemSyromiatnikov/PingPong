using System.Collections.Generic;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public interface IGamesService
    {
        Task<List<Game>> GetGames();
        Task<Game> CreateGame(CreateGameRequest newGameRequest);
    }
}