using Betsson.OnlineWallets.ApiTesting.Models;
using Betsson.OnlineWallets.ApiTesting.Models.Generic;

namespace Betsson.OnlineWallets.ApiTesting.Services
{
    public interface IOnlineWalletService
    {
        Task<ApiResponse<BalanceResponse, ErrorResponse>>  GetBalanceAsync();
        Task<ApiResponse<BalanceResponse, ErrorResponse>> DepositFundsAsync(string amount);
        Task<ApiResponse<BalanceResponse, ErrorResponse>>  WithdrawFundsAsync(string amount);
        Task<ApiResponse<BalanceResponse, ErrorResponse>>  ResetBalance(decimal targetBalance);
    }
}