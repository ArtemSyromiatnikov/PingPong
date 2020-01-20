using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp.Services
{
    public class PlayersService : IPlayersService
    {
        public List<PlayerInfo> Players =
            new List<PlayerInfo>
            {
                new PlayerInfo() {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5},
                new PlayerInfo() {Id = 2, FirstName = "Martin", LastName = "Stewart", Wins = 9, Losses = 1},
                new PlayerInfo() {Id = 3, FirstName = "Professor", LastName = "McGonagall", Wins = 6, Losses = 5},
                new PlayerInfo() {Id = 4, FirstName = "Professor", LastName = "Flitwick", Wins = 0, Losses = 0},
            };
        
        public async Task<List<PlayerInfo>> GetPlayers()
        {
            await Task.Delay(1000);
            
            return Players;
        }

        public async Task<PlayerInfo> GetPlayerById(int playerId)
        {
            await Task.Delay(1000);

            var player = Players.FirstOrDefault(p => p.Id == playerId);
            return player;
        }

        public async Task<PlayerInfo> CreatePlayer(CreatePlayerRequest request)
        {
            await Task.Delay(1000);

            var maxId = Players.Max(p => p.Id);
            var player = new PlayerInfo
            {
                Id        = maxId + 1,
                FirstName = request.FirstName,
                LastName  = request.LastName,
                Wins      = 0,
                Losses    = 0
            };
            
            Players.Add(player);

            return player;
        }
    }
}