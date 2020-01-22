using System.Net.Http;
using PingPong.Sdk.Helpers;
using PingPong.Sdk.Resources;

namespace PingPong.Sdk
{
    public class ApiClient : IApiClient
    {
        public IPlayersResource Players { get; }
        public IGamesResource   Games   { get; }


        public ApiClient(HttpClient httpClient)
        {
            var httpClientHelper = new HttpClientHelper(httpClient);

            Players = new PlayersResource(httpClientHelper);
            Games   = new GamesResource(httpClientHelper);
        }
    }
}