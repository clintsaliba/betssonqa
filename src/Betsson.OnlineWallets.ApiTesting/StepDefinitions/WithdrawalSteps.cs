using Betsson.OnlineWallets.ApiTesting.Services;
using TechTalk.SpecFlow;

namespace Betsson.OnlineWallets.ApiTesting.StepDefinitions
{
    [Binding]
    public class WithdrawalSteps
    {
        private readonly IOnlineWalletService _onlineWalletService;
        private readonly ScenarioContext _scenarioContext;

        public WithdrawalSteps(ScenarioContext scenarioContext, IOnlineWalletService onlineWalletService)
        {
            _onlineWalletService = onlineWalletService;
            _scenarioContext = scenarioContext;
        }
        
        [When(@"I withdraw funds from the wallet with amount '(.*)'")]
        public async Task WhenIWithdrawFundsFromTheWalletWithAmount(string amount)
        {
            var balance = await _onlineWalletService.WithdrawFundsAsync(amount);
            _scenarioContext["Response"] = balance;
        }
    }
}