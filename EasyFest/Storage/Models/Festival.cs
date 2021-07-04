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
    //[Node(
    //    IdField = nameof(Id),
    //    NodeResolverType = typeof(FestivalResolver),
    //    NodeResolver = nameof(FestivalResolver))
    //    ]
    public class Festival
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public double Rate { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        //public IReadOnlyList<FestivalLocation> FestivalLocations { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        //public List<string> FestivalLocations { get; set; }

        //[BsonIgnore]
        public ICollection<FestivalLocation> FestivalLocationsList { get; set; } = new List<FestivalLocation>();

        //[BsonRepresentation(BsonType.ObjectId)]
        //public List<string> Rates { get; set; }

        //[BsonIgnore]
        public IList<Rate> RatesList { get; set; }

        //[BsonRepresentation(BsonType.ObjectId)]
        //public List<string> Comments { get; set; }

        //[BsonIgnore]
        public IList<Comment> CommentsList { get; set; }
    }

    //public class FestivalResolver
    //{
    //    public Task<Festival> ResolveAsync(
    //        [Service] IFestivalLocationsService festLocation,
    //        Guid id)
    //    {
    //        return festLocation..Find(x => x.Id == id).FirstOrDefaultAsync();
    //    }
    //}
}
