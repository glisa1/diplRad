using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyFest.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        //public string Password { get; set; }

        //public string Salt { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5)]
        public string Password { get; set; }

        public bool HasError { get; set; }
    }
}
