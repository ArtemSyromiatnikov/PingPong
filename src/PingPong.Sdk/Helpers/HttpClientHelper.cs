using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PingPong.Sdk.Helpers
{
    internal class HttpClientHelper
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas         = true,
            PropertyNameCaseInsensitive = true,
            WriteIndented               = false
        };

        public HttpClientHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var payloadStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<TResponse>(payloadStream, _jsonOptions);
                return result;
            }

            throw new Exception($"Http GET failed with code: {(int) response.StatusCode}");
        }

        public async Task<TResponse> PostAsync<TResponse>(string url, object payload)
        {
            var requestBody = JsonSerializer.Serialize(payload);
            var content = new StringContent(requestBody, Encoding.Unicode, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var payloadStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<TResponse>(payloadStream, _jsonOptions);
                return result;
            }

            throw new Exception($"Http POST failed with code: {(int) response.StatusCode}");
        }
    }
}