using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            Images = new List<string>();
            Tags = new List<Tag>();
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

        //[JsonProperty("imageName")]
        //public string ImageName { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }
                
        //Gets rate given to certain festival by current user.
        [JsonProperty("rateByCurrentUser")]
        public double RateByCurrentUser { get; set; }

        [JsonProperty("numberOfComments")]
        public int NumberOfComments { get; set; }

        //[JsonProperty("festivalLocation")]
        //public FestivalLocation FestivalLocation { get; set; }

        [JsonProperty("commentsList")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("tagsList")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("billingDayStart")]
        public int BillingDayStart { get; set; }

        [JsonProperty("billingDayEnd")]
        public int BillingDayEnd { get; set; }

        [JsonProperty("billingMonthStart")]
        public int BillingMonthStart { get; set; }

        [JsonProperty("billingMonthEnd")]
        public int BillingMonthEnd { get; set; }

        #region Location

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

        #endregion
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

    public class FestivalWithTags
    {
        [JsonProperty("festivalById")]
        public Festival Festival { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }
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

    #region Pagination

    public class FestivalPaginate
    {
        [JsonProperty("festivals")]
        public FestivalNodes FestivalNodes { get; set; }
    }

    public class FestivalNodes
    {
        [JsonProperty("edges")]
        public IEnumerable<FestivalEdge> FestivalEdges { get; set; }

        [JsonProperty("pageInfo")]
        public PageInfo InfoPage { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }

    public class FestivalEdge
    {
        [JsonProperty("cursor")]
        public string CursorName { get; set; }

        [JsonProperty("node")]
        public Festival Festival { get; set; }
    }

    public class PageInfo
    {
        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; }
    }

    #endregion

    public class NewFestivalViewModel
    {
        public NewFestivalViewModel()
        {
            Images = new List<string>();
            UploadedImages = new List<IFormFile>();
            SelectedTags = new List<string>();
        }

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

        //public string ImageName { get; set; }

        public List<string> Images { get; set; }

        [Required]
        [Display(Name = "File")]
        public List<IFormFile> UploadedImages { get; set; }

        [Display(Name = "Festival tags")]
        public List<Tag> TagsList { get; set; }

        public List<string> SelectedTags { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket sale start")]
        public DateTime BillingDateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket sale end")]
        public DateTime BillingDateEnd { get; set; }

        public FestivalLocationViewModel FestivalLocation { get; set; }
    }

    public class UpdateFestivalViewModel
    {
        public UpdateFestivalViewModel()
        {
            Images = new List<string>();
            UploadedImages = new List<IFormFile>();
            SelectedTags = new List<string>(); 
        }
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

        //public string ImageName { get; set; }

        public List<string> Images { get; set; }

        [Display(Name = "Files")]
        public List<IFormFile> UploadedImages { get; set; }

        [Display(Name = "Festival tags")]
        public List<Tag> TagsList { get; set; }

        public List<string> SelectedTags { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket sale start")]
        public DateTime BillingDateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket sale end")]
        public DateTime BillingDateEnd { get; set; }

        public FestivalLocationViewModel FestivalLocation { get; set; }
    }
}
