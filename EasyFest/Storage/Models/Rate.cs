using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Models
{
    public class Rate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        //public string FestivalId { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        //public string UserId { get; set; }

        public double RateValue { get; set; }

        public Festival Festival { get; set; }

        public User User { get; set; }
    }
}
