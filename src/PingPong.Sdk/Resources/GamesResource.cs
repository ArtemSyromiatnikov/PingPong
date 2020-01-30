using System.Threading.Tasks;
using PingPong.Sdk.Helpers;
using PingPong.Sdk.Models;
using PingPong.Sdk.Models.Games;

namespace PingPong.Sdk.Resources
{
    internal class GamesResource : IGamesResource
    {
        private readonly HttpClientHelper _httpClient;

        public GamesResource(HttpClientHelper httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Page<GameDto>> GetGames(int page = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetAsync<Page<GameDto>>($"/games?page={page}&pageSize={pageSize}");
            return ReturnOrThrow(response);
        }

        public async Task<GameDto> GetGame(int id)
        {
            var response = await _httpClient.GetAsync<GameDto>($"/games/{id}");
            return ReturnOrThrow(response);
        }

        public async Task<GameDto> CreateGame(CreateGameRequestDto game)
        {
            var response = await _httpClient.PostAsync<GameDto>($"/games/", game);
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