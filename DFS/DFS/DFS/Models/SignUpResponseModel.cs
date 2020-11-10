using System;
using Newtonsoft.Json;

namespace DFS.Models
{
    public class SignUpResponseModel
    {
        [JsonProperty("Status")]
        public Status status { get; set; }

        public class Status
        {
            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("statusCode")]
            public string statusCode { get; set; }

            [JsonProperty("isStripeAccountCreated")]
            public string IsStripeAccountCreated { get; set; }

            [JsonProperty("stripeRedirectUrl")]
            public string StripeRedirectUrl { get; set; }


        }
    }
}
