using HotChocolate;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Storage.Services.FestivalLocationsService;
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
        public int NumberOfComments { get; set; }

        public int Month { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public int Day { get; set; }
        
        [BsonIgnore]
        public FestivalLocation FestivalLocation { get; set; }
        
        [BsonIgnore]
        public ICollection<Rate> RatesList { get; set; } = new List<Rate>();

        [BsonIgnore]
        public ICollection<Comment> CommentsList { get; set; } = new List<Comment>();
    }
}
