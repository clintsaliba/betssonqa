using Betsson.OnlineWallets.E2ETests.Services;
using Betsson.OnlineWallets.Models;
using TechTalk.SpecFlow;
using Xunit;

namespace Betsson.OnlineWallets.E2ETests.StepDefinitions
{
    [Binding]
    public class OnlineWalletServiceE2ESteps
    {
        private IOnlineWalletService _onlineWalletService;
        private readonly ScenarioContext _scenarioContext;
        
        public OnlineWalletServiceE2ESteps(ScenarioContext scenarioContext, IOnlineWalletService onlineWalletService)
        {
            _scenarioContext = scenarioContext;
            _onlineWalletService = onlineWalletService;
        }
        
        [Given(@"the wallet balance is initialized to (.*)")]
        public async Task GivenTheWalletBalanceIsInitializedTo(decimal targetBalance)
        {
            var balance = await _onlineWalletService.ResetBalance(targetBalance);
            _scenarioContext["CurrentBalance"] = balance;
        }

        [When(@"I retrieve the balance")]
        public async Task WhenIRetrieveTheBalance()
        {
            var balance = await _onlineWalletService.GetBalanceAsync();
            _scenarioContext["CurrentBalance"] = balance;
        }
        
        [When(@"I deposit funds in the wallet with amount (.*)")]
        public async Task WhenIDepositFundsInTheWalletWithAmount(decimal amount)
        {
            var balance = await _onlineWalletService.DepositFundsAsync(amount);
            _scenarioContext["CurrentBalance"] = balance;
        }
        
        [When(@"I withdraw funds from the wallet with amount (.*)")]
        public async Task WhenIWithdrawFundsFromTheWalletWithAmount(decimal amount)
        {
            var balance = await _onlineWalletService.WithdrawFundsAsync(amount);
            _scenarioContext["CurrentBalance"] = balance;
        }
        
        [Then(@"the balance should be (.*)")]
        public void ThenTheBalanceShouldBe(decimal expectedBalance)
        {
            var balance = _scenarioContext["CurrentBalance"] as Balance;
            
            Assert.NotNull(balance);
            Assert.Equal(expectedBalance, balance.Amount);
        }
    }
}