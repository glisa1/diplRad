using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public class Comment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("commentBody")]
        public string CommentBody { get; set; }

        [JsonProperty("festival")]
        public Festival Festival { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }
    }
}
