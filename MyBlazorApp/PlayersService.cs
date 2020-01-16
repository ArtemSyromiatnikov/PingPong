using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlazorApp.Models;

namespace MyBlazorApp
{
    public class PlayersService : IPlayersService
    {
        public List<PlayerStats> Players =
            new List<PlayerStats>
            {
                new PlayerStats() {Id = 1, FirstName = "Artem", LastName = "Syromiatnikov", Wins = 1, Losses = 5},
                new PlayerStats() {Id = 2, FirstName = "Martin", LastName = "Stewart", Wins = 9, Losses = 1},
                new PlayerStats() {Id = 3, FirstName = "Professor", LastName = "McGonagall", Wins = 6, Losses = 5},
                new PlayerStats() {Id = 4, FirstName = "Professor", LastName = "Flitwick", Wins = 0, Losses = 0},
            };
        
        public async Task<List<PlayerStats>> GetPlayerStats()
        {
            await Task.Delay(1000);
            
            return Players;
        }

        public async Task<PlayerStats> CreatePlayer(PlayerStats player)
        {
            await Task.Delay(1000);

            var maxId = Players.Max(p => p.Id);
            player.Id = maxId + 1;
            Players.Add(player);

            return player;
        }
    }
}