using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Storage.Models
{
    public class FestivalLocation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string FestivalId { get; set; }

        public Festival Festival { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
