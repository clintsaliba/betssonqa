using System.Text.Json.Serialization;

namespace Betsson.OnlineWallets.ApiTesting.Models.Generic
{
    public class Errors
    {
        [JsonPropertyName("depositRequest")]
        public List<string> DepositRequest { get; set; }
        
        [JsonPropertyName("withdrawRequest")]
        public List<string> WithdrawalRequest { get; set; }
    }
}