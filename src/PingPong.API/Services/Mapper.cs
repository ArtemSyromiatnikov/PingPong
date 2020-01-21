using System.Collections.Generic;
using System.Linq;
using PingPong.API.Database.Models;
using PingPong.Sdk.Models.Players;
using Game = PingPong.Sdk.Models.Games.Game;
using PlayerResult = PingPong.Sdk.Models.Games.PlayerResult;
using Player = PingPong.Sdk.Models.Games.Player;

namespace PingPong.API.Services
{
    /// <summary>
    /// Consider using AutoMapper 
    /// </summary>
    public static class Mapper
    {
        public static List<PlayerInfo> Map(List<Database.Models.Player> players)
        {
            return players?.Select(MapPlayerInfo).ToList();
        }

        public static PlayerInfo MapPlayerInfo(Database.Models.Player player)
        {
            return new PlayerInfo
            {
                Id        = player.Id,
                FirstName = player.FirstName,
                LastName  = player.LastName,
                Wins      = player.Wins,
                Losses    = player.Losses,
                Total     = player.Total
            };
        }

        public static List<Game> Map(List<Database.Models.Game> games)
        {
            return games?.Select(Map).ToList();
        }
        public static Game Map(Database.Models.Game game)
        {
            return new Game
            {
                Id            = game.Id,
                Timestamp     = game.Created,
                Player1Result = Map(game.Player1Result),
                Player2Result = Map(game.Player2Result)
            };
        }

        private static PlayerResult Map(Database.Models.PlayerResult gamePlayerResult)
        {
            return new PlayerResult
            {
                Player   = MapPlayer(gamePlayerResult.Player),
                Score    = gamePlayerResult.Score,
                IsWinner = gamePlayerResult.IsWinner
            };
        }
        public static Player MapPlayer(Database.Models.Player player)
        {
            return new Player
            {
                Id        = player.Id,
                FirstName = player.FirstName,
                LastName  = player.LastName
            };
        }
    }
}