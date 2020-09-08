using System.Text.Json.Serialization;

namespace Example.Client.Models
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
