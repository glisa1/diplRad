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
