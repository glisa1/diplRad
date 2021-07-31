using Newtonsoft.Json;
using System.Collections.Generic;

namespace EasyFest.Models
{
    public class ResponseData<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("errors")]
        public List<ErrorMessage> Errors {get;set;}
    }

    public class ErrorMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("extensions")]
        public ErrorExtensions Extensions { get; set; }
    }

    public class ErrorExtensions
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
