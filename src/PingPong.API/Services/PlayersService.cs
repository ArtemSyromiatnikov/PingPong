using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingPong.API.Database;
using PingPong.API.Database.Models;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Services
{
    public class PlayersService : IPlayersService
    {
        private readonly DataContext _dataContext;

        public PlayersService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        public async Task<Page<PlayerInfoDto>> GetPlayers(int page = 1, int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;
            
            int skip = pageSize * (page - 1);
            var query = _dataContext.Players
                .OrderBy(pl => pl.FirstName)
                .ThenBy(pl => pl.LastName);

            var total = await query.CountAsync();
            var players = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var result = new Page<PlayerInfoDto>(total, Mapper.Map(players));
            return result;
        }

        public async Task<PlayerInfoDto> GetPlayerById(int playerId)
        {
            var player = await _dataContext.Players
                .Where(p => p.Id == playerId)
                .FirstOrDefaultAsync();

            var result = Mapper.MapPlayerInfo(player);
            return result;
        }

        public async Task<PlayerInfoDto> CreatePlayer(CreatePlayerRequestDto request)
        {
            var player = new Player
            {
                FirstName = request.FirstName,
                LastName  = request.LastName,
                Wins      = 0,
                Losses    = 0,
                Total     = 0,
                Created   = DateTime.Now,
                Modified  = DateTime.Now
            };
            
            _dataContext.Players.Add(player);
            await _dataContext.SaveChangesAsync();
        
            return Mapper.MapPlayerInfo(player);
        }
    }
}