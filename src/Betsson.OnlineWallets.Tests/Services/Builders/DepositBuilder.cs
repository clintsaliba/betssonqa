using Betsson.OnlineWallets.Models;

namespace Betsson.OnlineWallets.Tests.Services.Builders
{
    public class DepositBuilder
    {
        private decimal _amount = 0;

        public DepositBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public Deposit Build()
        {
            return new Deposit
            {
                Amount = _amount
            };
        }
    }
}