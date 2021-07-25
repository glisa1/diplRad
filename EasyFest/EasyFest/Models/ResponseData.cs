using Newtonsoft.Json;

namespace EasyFest.Models
{
    public class ResponseData<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
