using HotChocolate;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string ImageId { get; set; }

        public bool IsAdmin { get; set; }

        public List<string> Tags { get; set; }

        public List<string> SubscribedFests { get; set; }

        [BsonIgnore]
        public ICollection<Storage.Models.Tag> TagsList { get; set; } = new List<Storage.Models.Tag>();

        [BsonIgnore]
        public ICollection<Storage.Models.Festival> SubscribedFestsList { get; set; } = new List<Storage.Models.Festival>();

        [BsonIgnore]
        public int CommentsPostedByUser { get; set; }

        [BsonIgnore]
        public int RatesGivenByUser { get; set; }

        [GraphQLIgnore]
        public string Password { get; set; }

        [GraphQLIgnore]
        public string Salt { get; set; }
    }
}
