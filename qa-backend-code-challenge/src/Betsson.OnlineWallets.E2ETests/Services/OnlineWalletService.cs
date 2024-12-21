using Betsson.OnlineWallets.Models;

namespace Betsson.OnlineWallets.E2ETests.Services
{
    public class OnlineWalletService: IOnlineWalletService
    {
        private readonly IApiClient _apiClient;
        private HttpResponseMessage _response;
        private decimal _balance;

        public OnlineWalletService(IApiClient apiClient)
        {
            _apiClient = apiClient;
            _balance = 0;
        }

        public async Task<Balance> GetBalanceAsync()
        {
            return await _apiClient.GetAsync<Balance>("/wallet/balance");
        }
        
        public async Task<Balance> DepositFundsAsync(decimal amount)
        {
            var depositRequest = new { Amount = amount };
            return await _apiClient.PostAsync<object, Balance>("/wallet/deposit", depositRequest);
        }

        public async Task<Balance> WithdrawFundsAsync(decimal amount)
        {
            var withdrawalRequest = new { Amount = amount };
            return await _apiClient.PostAsync<object, Balance>("/wallet/withdraw", withdrawalRequest);
        }

        public async Task<Balance> ResetBalance(decimal targetBalance)
        {
            Balance currentBalance = await GetBalanceAsync();
            decimal currentBalanceAmount = currentBalance.Amount;

            decimal difference = targetBalance - currentBalanceAmount;

            if (difference > 0)
            {
                return await DepositFundsAsync(difference);
            }

            if (difference < 0)
            {
                return await WithdrawFundsAsync(Math.Abs(difference));
            }

            return currentBalance;
        }
    }
}