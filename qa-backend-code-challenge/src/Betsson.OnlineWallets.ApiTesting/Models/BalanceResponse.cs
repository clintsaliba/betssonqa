using System.Text.Json.Serialization;

namespace Betsson.OnlineWallets.ApiTesting.Models
{
    public class BalanceResponse
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}