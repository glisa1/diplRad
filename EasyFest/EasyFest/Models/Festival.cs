﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

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

        [JsonProperty("endDay")]
        public int EndDay { get; set; }

        [JsonProperty("endMonth")]
        public int EndMonth { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageName")]
        public string ImageName { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
                
        //Gets rate given to certain festival by current user.
        [JsonProperty("rateByCurrentUser")]
        public double RateByCurrentUser { get; set; }

        [JsonProperty("numberOfComments")]
        public int NumberOfComments { get; set; }

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

    public class FestivalForQuery
    {
        [JsonProperty("festival")]
        public Festival Festival { get; set; }
    }

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

    public class UserRateForFestival
    {
        [JsonProperty("userRateForFestival")]
        public double Rate { get; set; }
    }

    public class NewFestivalViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile Image { get; set; }

        public FestivalLocationViewModel FestivalLocation { get; set; }
    }

    public class UpdateFestivalViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string OldName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [Display(Name = "File")]
        public IFormFile Image { get; set; }

        public FestivalLocationViewModel FestivalLocation { get; set; }
    }
}
