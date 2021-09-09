using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    /// <summary>
    /// Used for settings page.
    /// </summary>
    public class SettingsPage
    {
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("users")]
        public List<User> User { get; set; }
    }
}
