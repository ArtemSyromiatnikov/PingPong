using System.Threading.Tasks;
using PingPong.Sdk.Helpers;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Players;

namespace PingPong.Sdk.Resources
{
    internal class PlayersResource : IPlayersResource
    {
        private readonly HttpClientHelper _httpClient;

        public PlayersResource(HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Page<PlayerInfoDto>> GetPlayers(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync<Page<PlayerInfoDto>>($"/players?page={page}&pageSize={pageSize}");
            return ReturnOrThrow(response);
        }

        public async Task<PlayerInfoDto> GetPlayerById(int playerId)
        {
            var response = await _httpClient.GetAsync<PlayerInfoDto>($"/players/{playerId}");
            return ReturnOrThrow(response);
        }

        public async Task<PlayerInfoDto> CreatePlayer(CreatePlayerRequestDto player)
        {
            var response = await _httpClient.PostAsync<PlayerInfoDto>($"/players/", player);
            return ReturnOrThrow(response);
        }
        
        private static T ReturnOrThrow<T>(ApiResponse<T> response)
        {
            if (response.IsSuccess)
                return response.Data;
            
            throw new ApiException(response.ErrorDto);
        }

    }
}