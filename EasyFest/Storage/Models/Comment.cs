using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Storage.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public string CommentBody { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string FestivalId { get; set; }

        public Festival Festival { get; set; }

        public User User { get; set; }
    }
}
