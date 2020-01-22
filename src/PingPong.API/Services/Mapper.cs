using System.Collections.Generic;
using System.Linq;
using PingPong.API.Database.Models;
using PingPong.Sdk.Models.Games;
using PingPong.Sdk.Models.Players;

namespace PingPong.API.Services
{
    /// <summary>
    /// Consider using AutoMapper 
    /// </summary>
    public static class Mapper
    {
        public static List<PlayerInfoDto> Map(List<Player> players)
        {
            return players?.Select(MapPlayerInfo).ToList();
        }

        public static PlayerInfoDto MapPlayerInfo(Player player)
        {
            return new PlayerInfoDto
            {
                Id        = player.Id,
                FirstName = player.FirstName,
                LastName  = player.LastName,
                Wins      = player.Wins,
                Losses    = player.Losses,
                Total     = player.Total
            };
        }

        public static List<GameDto> Map(List<Game> games)
        {
            return games?.Select(Map).ToList();
        }
        public static GameDto Map(Game game)
        {
            return new GameDto
            {
                Id            = game.Id,
                Timestamp     = game.Created,
                Player1Result = Map(game.Player1Result),
                Player2Result = Map(game.Player2Result)
            };
        }

        private static PlayerResultDto Map(PlayerResult gamePlayerResult)
        {
            return new PlayerResultDto
            {
                PlayerDto   = MapPlayer(gamePlayerResult.Player),
                Score    = gamePlayerResult.Score,
                IsWinner = gamePlayerResult.IsWinner
            };
        }
        public static PlayerDto MapPlayer(Player player)
        {
            return new PlayerDto
            {
                Id        = player.Id,
                FirstName = player.FirstName,
                LastName  = player.LastName
            };
        }
    }
}