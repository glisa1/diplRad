using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EasyFest.Models
{
    public class FestivalLocation
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class FestivalLocationViewModel
    {
        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
