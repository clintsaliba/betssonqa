using Betsson.OnlineWallets.ApiTesting.Services;
using TechTalk.SpecFlow;

namespace Betsson.OnlineWallets.ApiTesting.StepDefinitions
{
    [Binding]
    public class DepositSteps
    {
        private readonly IOnlineWalletService _onlineWalletService;
        private readonly ScenarioContext _scenarioContext;

        public DepositSteps(ScenarioContext scenarioContext, IOnlineWalletService onlineWalletService)
        {
            _onlineWalletService = onlineWalletService;
            _scenarioContext = scenarioContext;
        }

        [When(@"I deposit funds in the wallet with amount '(.*)'")]
        public async Task WhenIDepositFundsInTheWalletWithAmount(string amount)
        {
            var response = await _onlineWalletService.DepositFundsAsync(amount);
            _scenarioContext["Response"] = response;
        }
    }
}