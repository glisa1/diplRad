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

        [JsonProperty("commentsPostedByUser")]
        public int CommentsPostedByUser { get; set; }

        [JsonProperty("ratesGivenByUser")]
        public int RatesGivenByUser { get; set; }

        //public string Password { get; set; }
        //public string Salt { get; set; }
    }

    public class UserById
    {
        [JsonProperty("userById")]
        public User User { get; set; }
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

    public class UserRegisterModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5)]
        [Compare("PasswordRepeat")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Repeat password")]
        public string PasswordRepeat { get; set; }
    }
}
