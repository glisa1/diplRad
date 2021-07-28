using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyFest.Models
{
    public class Festival
    {
        public Festival()
        {
            Comments = new List<Comment>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("day")]
        public int Day { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("festivalLocation")]
        public FestivalLocation FestivalLocation { get; set; }

        [JsonProperty("commentsList")]
        public List<Comment> Comments { get; set; }
    }

    //public class FestivalWithLocation : Festival
    //{
    //    [JsonProperty("festivalLocation")]
    //    public FestivalLocation Location { get; set; }
    //}

    public class FestivalById
    {
        [JsonProperty("festivalById")]
        public Festival Festival { get; set; }
    }

    public class FestivalList
    {
        public FestivalList()
        {
            Festivals = new List<Festival>();
        }

        [JsonProperty("festivals")]
        public List<Festival> Festivals { get; set; }
    }
}
