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

    public class CreateRateModel
    {
        //string userId, string festivalId, double rateValue
        public string UserId { get; set; }

        public string FestivalId { get; set; }

        public double RateValue { get; set; }
    }


    //{"data":{"updateRate":{"festival":{"rate":3.5}}}}
    public class UpdateRateQueryModel
    {
        [JsonProperty("updateRate")]
        public FestivalForQuery Festival { get; set; }
    }

    public  class CreateRateQueryModel
    {
        [JsonProperty("createRate")]
        public FestivalForQuery Festival { get; set; }
    }
}
