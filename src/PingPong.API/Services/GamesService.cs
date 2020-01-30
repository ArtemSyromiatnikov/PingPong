using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingPong.API.Database;
using PingPong.Sdk;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;

namespace PingPong.API.Services
{
    public class GamesService : IGamesService
    {
        private readonly DataContext _dataContext;

        public GamesService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Page<GameDto>> GetGames(int page = 1, int pageSize = 10)
        {
            if (page < 1) page         = 1;
            if (pageSize < 1) pageSize = 1;
            
            int skip = pageSize * (page - 1);
            var query = _dataContext.Games
                .Include(g => g.Player1Result.Player)
                .Include(g => g.Player2Result.Player)
                .OrderByDescending(pl => pl.Created);

            var total = await query.CountAsync();
            var games = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            var result = new Page<GameDto>(total, Mapper.Map(games));
            return result;
        }

        public async Task<GameDto> GetGame(int id)
        {
            var game = await _dataContext.Games
                .Where(g => g.Id == id)
                .Include(g => g.Player1Result.Player)
                .Include(g => g.Player2Result.Player)
                .FirstOrDefaultAsync();

            var result = Mapper.Map(game);
            return result;
        }

        public async Task<GameDto> CreateGame(CreateGameRequestDto request)
        {
            var player1 = await _dataContext.Players.FirstOrDefaultAsync(p => p.Id == request.Player1Id);
            if (player1 == null)
                throw new ApiException(400, "Player 1 was not found", "PLAYER_NOT_FOUND");
            
            var player2 = await _dataContext.Players.FirstOrDefaultAsync(p => p.Id == request.Player2Id);
            if (player2 == null)
                throw new ApiException(400, "Player 2 was not found", "PLAYER_NOT_FOUND");

            bool isPlayer1Winner = request.Player1Score > request.Player2Score;
            var game = new Database.Models.Game()
            {
                Created = DateTime.Now,
                Player1Result = new Database.Models.PlayerResult()
                {
                    PlayerId = request.Player1Id,
                    Score    = request.Player1Score,
                    IsWinner = isPlayer1Winner
                },
                Player2Result = new Database.Models.PlayerResult()
                {
                    PlayerId = request.Player2Id,
                    Score    = request.Player2Score,
                    IsWinner = !isPlayer1Winner
                }
            };
            
            using (var transaction = await _dataContext.Database.BeginTransactionAsync())
            {
                try
                {
                    player1.Total++;
                    player2.Total++;
                    if (isPlayer1Winner)
                    {
                        player1.Wins++;
                        player2.Losses++;
                    }
                    else
                    {
                        player1.Losses++;
                        player2.Wins++;
                    }

                    _dataContext.Games.Add(game);
                    await _dataContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        
            return await GetGame(game.Id);
        }
    }
}