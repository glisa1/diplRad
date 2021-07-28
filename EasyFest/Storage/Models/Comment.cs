using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Storage.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public string CommentBody { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string FestivalId { get; set; }

        [BsonIgnore]
        public Festival Festival { get; set; }

        [BsonIgnore]
        public User User { get; set; }
    }
}
