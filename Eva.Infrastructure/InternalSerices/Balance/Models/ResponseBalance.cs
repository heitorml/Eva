using System.Text.Json.Serialization;

namespace Eva.Infrastructure.InternalSerices.Balance.Models
{

    public class ResponseBalance
    {
        [JsonPropertyName("card-balance")]
        public string? cardbalance { get; set; }
    }

}
