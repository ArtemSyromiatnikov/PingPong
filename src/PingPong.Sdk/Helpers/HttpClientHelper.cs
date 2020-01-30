using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PingPong.Sdk.Models;

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

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await ProcessResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TResponse>(string url, object payload)
        {
            var requestBody = JsonSerializer.Serialize(payload);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            return await ProcessResponse<TResponse>(response);
        }


        #region Private helpers

        private async Task<ApiResponse<TResponse>> ProcessResponse<TResponse>(HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = JsonSerializer.Deserialize<TResponse>(responseBody, _jsonOptions);
                    return ApiResponse<TResponse>.Success((int) response.StatusCode, result);
                }
                catch (JsonException)
                {
                    return ApiResponse<TResponse>.Error(500,
                        new ErrorDto(500, "Unable to deserialize response", "DESERIALIZATION_FAILED"));
                }
            }

            try
            {
                var error = JsonSerializer.Deserialize<ErrorDto>(responseBody, _jsonOptions);
                return ApiResponse<TResponse>.Error((int) response.StatusCode, error);
            }
            catch (JsonException)
            {
                return ApiResponse<TResponse>.Error((int) response.StatusCode,
                    new ErrorDto((int) response.StatusCode, "Unable to deserialize error response", "DESERIALIZATION_FAILED"));
            }
        }

        #endregion
    }
}