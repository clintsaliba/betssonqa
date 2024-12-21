using Betsson.OnlineWallets.ApiTesting.Client;
using Betsson.OnlineWallets.ApiTesting.Constants;
using Betsson.OnlineWallets.ApiTesting.Models;
using Betsson.OnlineWallets.ApiTesting.Models.Generic;

namespace Betsson.OnlineWallets.ApiTesting.Services
{
    public class OnlineWalletService : IOnlineWalletService
    {
        private readonly IApiClient _apiClient;

        public OnlineWalletService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<ApiResponse<BalanceResponse, ErrorResponse>> GetBalanceAsync()
        {
            return await SendRequestAsync<BalanceResponse>(ApiEndpoints.GetBalance, HttpMethod.Get);
        }

        public async Task<ApiResponse<BalanceResponse, ErrorResponse>> DepositFundsAsync(string amount)
        {
            return await SendRequestWithPayloadAsync(ApiEndpoints.DepositFunds, new { Amount = amount });
        }

        public async Task<ApiResponse<BalanceResponse, ErrorResponse>> WithdrawFundsAsync(string amount)
        {
            return await SendRequestWithPayloadAsync(ApiEndpoints.WithdrawFunds, new { Amount = amount });
        }

        public async Task<ApiResponse<BalanceResponse, ErrorResponse>> ResetBalance(decimal targetBalance)
        {
            var currentBalanceResponse = await GetBalanceAsync();
            if (currentBalanceResponse.Success == null)
                return currentBalanceResponse;

            decimal currentBalanceAmount = currentBalanceResponse.Success.Amount;
            decimal difference = targetBalance - currentBalanceAmount;

            if (difference > 0)
                return await DepositFundsAsync(difference.ToString());
            if (difference < 0)
                return await WithdrawFundsAsync(Math.Abs(difference).ToString());

            return currentBalanceResponse;
        }

        private async Task<ApiResponse<TSuccess, ErrorResponse>> SendRequestAsync<TSuccess>(string endpoint, HttpMethod method)
        {
            var response = await _apiClient.GetAsync<TSuccess, ErrorResponse>(endpoint);
            return CreateApiResponse(response.SuccessResponse, response.ErrorResponse);
        }

        private async Task<ApiResponse<BalanceResponse, ErrorResponse>> SendRequestWithPayloadAsync(string endpoint, object payload)
        {
            var response = await _apiClient.PostAsync<object, BalanceResponse, ErrorResponse>(endpoint, payload);
            return CreateApiResponse(response.SuccessResponse, response.ErrorResponse);
        }

        private ApiResponse<TSuccess, TError> CreateApiResponse<TSuccess, TError>(TSuccess success, TError error)
        {
            return new ApiResponse<TSuccess, TError>
            {
                Success = success,
                Error = error
            };
        }
    }
}
