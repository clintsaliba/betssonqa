using Betsson.OnlineWallets.Models;

namespace Betsson.OnlineWallets.E2ETests.Services
{
    public interface IOnlineWalletService
    {
        Task<Balance> GetBalanceAsync();
        Task<Balance> DepositFundsAsync(decimal amount);
        Task<Balance> WithdrawFundsAsync(decimal amount);
        Task<Balance> ResetBalance(decimal targetBalance);
    }
}