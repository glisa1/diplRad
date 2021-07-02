using HotChocolate;
using MongoDB.Driver;
using Storage.Services.FestivalService;
using System.Linq;

namespace GraphQLDataAccess.Schema
{
    public class Query
    {
        private readonly IFestivalService _festivalService;

        public Query(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        //public Festival GetFestival() =>
        //    new Festival
        //    {
        //        Name = "TestName"
        //    };

        //public Festival GetFestival()
        //{
        //    var fest = _festivalService.GetFestival("60db8b0061efa5f8cab3762b");
        //    return new Festival { Id = fest.Id, Name = fest.Name };
        //}

        public IExecutable<Storage.Models.Festival> GetFestivals()
        {
            return _festivalService.GetFestivals();
        }

        //public IMongoCollection<Storage.Models.Festival> GetFestivals([Service] FestivalService festivalService)
        //{
        //    return festivalService.Festivals;
        //}

        //public IQueryable<Storage.Models.Festival> GetFestivals([Service] IFestivalService festivalService)
        //{
        //    return festivalService.Festivals.AsQueryable();
        //}
    }
}
