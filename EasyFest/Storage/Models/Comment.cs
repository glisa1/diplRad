using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Storage.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string CommentBody { get; set; }

        public int Rate { get; set; }

        public string FestivalId { get; set; }
    }
}
