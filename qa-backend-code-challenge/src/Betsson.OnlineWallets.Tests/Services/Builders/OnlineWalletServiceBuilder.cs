using Betsson.OnlineWallets.Data.Models;

namespace Betsson.OnlineWallets.Tests.Services.Builders
{
    public class OnlineWalletServiceBuilder
    {
        private decimal _amount = 0;
        private decimal _balanceBefore = 0;
        private DateTimeOffset _eventTime = DateTimeOffset.UtcNow;

        public OnlineWalletServiceBuilder WithAmount(decimal amount)
        {
            _amount = amount;
            return this;
        }

        public OnlineWalletServiceBuilder WithBalanceBefore(decimal balanceBefore)
        {
            _balanceBefore = balanceBefore;
            return this;
        }
        
        public OnlineWalletEntry Build()
        {
            return new OnlineWalletEntry
            {
                Amount = _amount,
                BalanceBefore = _balanceBefore,
                EventTime = _eventTime
            };
        }
    }
}