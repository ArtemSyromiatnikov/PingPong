using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PingPong.Sdk;
using PingPong.Sdk.Models.Players;

namespace PingPong.Blazor.Services
{
    public class PlayersService : IPlayersService
    {
        public List<PlayerInfoDto> Players =
            new List<PlayerInfoDto>
            {
                new PlayerInfoDto() {Id = 1, FirstName = "Artem", LastName     = "Syromiatnikov", Wins = 1, Losses = 5, Total = 6},
                new PlayerInfoDto() {Id = 2, FirstName = "Martin", LastName    = "Stewart", Wins       = 9, Losses = 1, Total = 10},
                new PlayerInfoDto() {Id = 3, FirstName = "Professor", LastName = "McGonagall", Wins    = 6, Losses = 5, Total = 11},
                new PlayerInfoDto() {Id = 4, FirstName = "Professor", LastName = "Flitwick", Wins      = 0, Losses = 0, Total = 0},
            };
        
        public async Task<List<PlayerInfoDto>> GetPlayers()
        {
            await Task.Delay(1000);
            
            return Players;
        }

        public async Task<PlayerInfoDto> GetPlayerById(int playerId)
        {
            await Task.Delay(1000);

            var player = Players.FirstOrDefault(p => p.Id == playerId);
            return player;
        }

        public async Task<PlayerInfoDto> CreatePlayer(CreatePlayerRequestDto request)
        {
            await Task.Delay(1000);

            var maxId = Players.Max(p => p.Id);
            var player = new PlayerInfoDto
            {
                Id        = maxId + 1,
                FirstName = request.FirstName,
                LastName  = request.LastName,
                Wins      = 0,
                Losses    = 0,
                Total     = 0
            };
            
            Players.Add(player);

            return player;
        }
    }
}