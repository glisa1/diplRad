using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public class Rate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("festivalId")]
        public string FestivalId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("rateValue")]
        public double RateValue { get; set; }

        [JsonProperty("festival")]
        public Festival Festival { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}
