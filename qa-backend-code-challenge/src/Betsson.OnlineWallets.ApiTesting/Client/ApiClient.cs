using System.Text;
using System.Text.Json;

namespace Betsson.OnlineWallets.ApiTesting.Client
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<(TResponse SuccessResponse, TErrorResponse ErrorResponse)> GetAsync<TResponse, TErrorResponse>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);
            return await ProcessResponse<TResponse, TErrorResponse>(response);
        }

        public async Task<(TResponse SuccessResponse, TErrorResponse ErrorResponse)> PostAsync<TRequest, TResponse, TErrorResponse>(
            string endpoint, TRequest payload)
        {
            var jsonContent = CreateJsonContent(payload);
            var response = await _httpClient.PostAsync(endpoint, jsonContent);
            return await ProcessResponse<TResponse, TErrorResponse>(response);
        }

        private StringContent CreateJsonContent<TRequest>(TRequest payload) =>
            new StringContent(JsonSerializer.Serialize(payload, _jsonOptions), Encoding.UTF8, "application/json");

        private async Task<(TResponse SuccessResponse, TErrorResponse ErrorResponse)> ProcessResponse<TResponse, TErrorResponse>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var successResponse = JsonSerializer.Deserialize<TResponse>(content, _jsonOptions);
                return (successResponse, default);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<TErrorResponse>(content, _jsonOptions);
                return (default, errorResponse);
            }
        }
    }
}
