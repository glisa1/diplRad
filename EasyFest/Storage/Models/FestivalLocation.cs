using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Storage.Models
{
    public class FestivalLocation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FestivalId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
