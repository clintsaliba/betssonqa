using Betsson.OnlineWallets.Models;

namespace Betsson.OnlineWallets.Tests.Services.Builders
{
    public class WithdrawalBuilder
    {
        private decimal _amount = 0;

        public WithdrawalBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public Withdrawal Build()
        {
            return new Withdrawal
            {
                Amount = _amount
            };
        }
    }
}