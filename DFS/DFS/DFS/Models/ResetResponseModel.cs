using System;
using Newtonsoft.Json;

namespace DFS.Models
{
    public class ResetResponseModel
    {
        [JsonProperty("StatusInfo")]
        public StatusInfo statusInfo { get; set; }

        public class StatusInfo
        {
            [JsonProperty("Status")]
            public string Status { get; set; }

        }
    }
}
