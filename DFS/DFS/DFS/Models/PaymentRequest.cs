using System;
using Newtonsoft.Json;

namespace DFS.Models
{
    public class PaymentRequest
    {
        [JsonProperty("businessTransaction")]
        public BusinessTransaction BusinessTransaction { get; set; }
    }

    public class BusinessTransaction
    {
        [JsonProperty("payeeId")]
        public string PayeeId { get; set; }

        [JsonProperty("payerId")]
        public string PayerId { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("eventId")]
        public string EventId { get; set; }
    }
}
