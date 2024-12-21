using Betsson.OnlineWallets.ApiTesting.Models;
using Betsson.OnlineWallets.ApiTesting.Models.Generic;
using Betsson.OnlineWallets.ApiTesting.Services;
using TechTalk.SpecFlow;

[Binding]
public class GenericSteps
{
    private readonly IOnlineWalletService _onlineWalletService;
    private readonly ScenarioContext _scenarioContext;

    public GenericSteps(IOnlineWalletService onlineWalletService, ScenarioContext scenarioContext)
    {
        _onlineWalletService = onlineWalletService;
        _scenarioContext = scenarioContext;
    }

    [Given(@"the wallet balance is initialized to '(.*)'")]
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

    [Then(@"the balance should be '(.*)'")]
    public void ThenTheBalanceShouldBe(decimal expectedBalance)
    {
        var balance = _scenarioContext["CurrentBalance"] as ApiResponse<BalanceResponse, ErrorResponse>;
        Assert.NotNull(balance);
        Assert.Equal(expectedBalance, balance.Success.Amount);
    }

    [Then(@"the correct '(.*)' error is shown with status '(.*)'")]
    public void ThenTheCorrectErrorIsShown(string error, int status)
    {
        var balance = _scenarioContext["Response"] as ApiResponse<BalanceResponse, ErrorResponse>;
        Assert.NotNull(balance);
        Assert.Equal(balance.Error.Title, error);
        Assert.Equal(balance.Error.Status, status);
    }
}