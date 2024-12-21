namespace Betsson.OnlineWallets.ApiTesting.Client
{
    public interface IApiClient
    {
        Task<(TResponse SuccessResponse, TErrorResponse ErrorResponse)> GetAsync<TResponse, TErrorResponse>(string endpoint);
        Task<(TResponse SuccessResponse, TErrorResponse ErrorResponse)> PostAsync<TRequest, TResponse, TErrorResponse>(string endpoint, TRequest payload);
    }
}