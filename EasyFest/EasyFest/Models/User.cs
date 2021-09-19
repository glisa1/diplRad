using Microsoft.AspNetCore.Http;
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
        public User()
        {
            Tags = new List<Tag>();
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("commentsPostedByUser")]
        public int CommentsPostedByUser { get; set; }

        [JsonProperty("ratesGivenByUser")]
        public int RatesGivenByUser { get; set; }

        [JsonProperty("tags")]
        public List<string> SelectedTags { get; set; }

        [JsonProperty("tagsList")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("subscribedFestsList")]
        public List<Festival> SubscribedFests { get; set; }

        //public string Password { get; set; }
        //public string Salt { get; set; }
    }

    public class UserById
    {
        [JsonProperty("userById")]
        public User User { get; set; }
    }

    public class DeleteUserPayload
    {
        [JsonProperty("deleteUser")]
        public bool Status { get; set; }
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
        public UserRegisterModel()
        {
            SelectedTags = new List<string>();
            AllTags = new List<Tag>();
        }

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

        public List<string> SelectedTags { get; set; }

        public List<Tag> AllTags { get; set; }

        [Required]
        [Display(Name = "Image")]
        public IFormFile UploadedImage { get; set; }

        public string ImageId { get; set; }
    }

    public class MyProfileModel
    {
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("userById")]
        public User User { get; set; }
    }
}
