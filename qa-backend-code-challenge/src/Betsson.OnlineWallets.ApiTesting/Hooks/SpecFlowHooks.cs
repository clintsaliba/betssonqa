using Betsson.OnlineWallets.ApiTesting;
using Betsson.OnlineWallets.ApiTesting.Client;
using Betsson.OnlineWallets.ApiTesting.Services;
using Betsson.OnlineWallets.ApiTesting.Services.Configurations;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using BoDi;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

[Binding]
public class SpecFlowHooks
{
    private static IServiceProvider _serviceProvider;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var services = new ServiceCollection();

        services.AddHttpClient<IApiClient, ApiClient>(client =>
        {
            client.BaseAddress = new Uri(new ConfigurationService().GetBaseUrl() ?? string.Empty);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        services.AddScoped<IOnlineWalletService, OnlineWalletService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    [BeforeScenario]
    public void BeforeScenario(ScenarioContext scenarioContext, IObjectContainer container)
    {
        container.RegisterInstanceAs(_serviceProvider.GetRequiredService<IApiClient>());
        container.RegisterInstanceAs(_serviceProvider.GetRequiredService<IOnlineWalletService>());
    }
}