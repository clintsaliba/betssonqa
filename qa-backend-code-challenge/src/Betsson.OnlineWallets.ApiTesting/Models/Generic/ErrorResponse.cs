using System.Text.Json.Serialization;

namespace Betsson.OnlineWallets.ApiTesting.Models.Generic
{
    public class ErrorResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("status")]
        public int Status { get; set; }
        
        [JsonPropertyName("errors")]
        public Errors Errors { get; set; }
    }
}