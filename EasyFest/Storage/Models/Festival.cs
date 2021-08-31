using HotChocolate;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Storage.Services.FestivalService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class Festival
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        [BsonIgnore]
        public double Rate { get; set; }

        [BsonIgnore]
        public double RateByCurrentUser { get; set; }

        [BsonIgnore]
        public int NumberOfComments { get; set; }

        public int Month { get; set; }

        public string Description { get; set; }

        //public string ImageName { get; set; }

        public List<string> Images { get; set; }

        public int Day { get; set; }

        public int EndDay { get; set; }

        public int EndMonth { get; set; }
        
        //[BsonIgnore]
        //public FestivalLocation FestivalLocation { get; set; }
        
        [BsonIgnore]
        public ICollection<Rate> RatesList { get; set; } = new List<Rate>();

        [BsonIgnore]
        public ICollection<Comment> CommentsList { get; set; } = new List<Comment>();

        #region Location

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        #endregion
    }
}
