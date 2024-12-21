using Microsoft.Extensions.Configuration;

namespace Betsson.OnlineWallets.ApiTesting.Services.Configurations
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public string? GetBaseUrl()
        {
            return _configuration.GetSection("ApiSettings:BaseUrl").Value;
        }
    }
}