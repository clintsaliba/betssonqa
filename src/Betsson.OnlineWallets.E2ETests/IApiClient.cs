namespace Betsson.OnlineWallets.E2ETests
{
    public interface IApiClient
    {
        Task<TResponse> GetAsync<TResponse>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest payload);
    }
}