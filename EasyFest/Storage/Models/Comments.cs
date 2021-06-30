using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Storage.Models
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }

        public int Rate { get; set; }

        public string FestivalId { get; set; }
    }
}
