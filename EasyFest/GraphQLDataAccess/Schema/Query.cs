using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MongoDB.Driver;
using Storage.Services.CommentsService;
using Storage.Services.FestivalService;
using Storage.Services.RateService;
using Storage.Services.UserService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema
{
    public class Query : IQuery
    {
        #region Init

        private readonly IFestivalService _festivalService;
        private readonly ICommentsService _commentsService;
        private readonly IRateService _rateService;
        private readonly IUserService _usersService;

        public Query(IFestivalService festivalService,
                                ICommentsService festivalLocationsService,
                                IRateService rateService,
                                IUserService usersService)
        {
            _festivalService = festivalService;
            _commentsService = festivalLocationsService;
            _rateService = rateService;
            _usersService = usersService;
        }

        #endregion

        #region Methods

        //[UsePaging(IncludeTotalCount = true)]
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //public IExecutable<Storage.Models.Festival> GetFestivals()
        //{
        //    return _festivalService.GetFestivals().AsExecutable();
        //}

        //[UsePaging(IncludeTotalCount = true)]
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //public IEnumerable<Storage.Models.Festival> GetFestivals()
        //{
        //    return _festivalService.GetFestivals().Source;
        //}

        [UsePaging(IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Storage.Models.Festival> GetFestivals()
        {
            return _festivalService.GetFestivals();
        }

        [UseProjection]
        public IExecutable<Storage.Models.Festival> GetFestivalsInfo()
        {
            return _festivalService.GetFestivalsExec();
        }

        //[UseProjection]
        //public IExecutable<Storage.Models.Festival> GetFestivalById(string id)
        //{
        //    return _festivalService.GetFestivalById(id);
        //}

        [UseProjection]
        public async Task<Storage.Models.Festival> GetFestivalById(string id)
        {
            return await _festivalService.GetFestivalByIdAsync(id);
        }

        [UseProjection]
        public async Task<Storage.Models.User> GetUserById(string id)
        {
            return await _usersService.GetUserWithIdAsync(id);
        }

        //[UsePaging(IncludeTotalCount = true)]
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //public IExecutable<Storage.Models.FestivalLocation> GetFestivalLocations()
        //{
        //    return _festivalLocationsService.GetFestivalLocations();
        //}

        //[UsePaging(IncludeTotalCount = true)]
        //[UseFiltering]
        //[UseSorting]
        //public IQueryable<Storage.Models.FestivalLocation> GetFestivalLocations()
        //{
        //    return _festivalLocationsService.GetFestivalLocationsQueryable();
        //}

        //public IExecutable<Storage.Models.FestivalLocation> GetFestivalLocationsInfo()
        //{
        //    return _festivalLocationsService.GetFestivalLocations();
        //}

        [UseProjection]
        public IExecutable<Storage.Models.Comment> GetComments()
        {
            return _commentsService.GetComments();
        }

        [UseProjection]
        public IExecutable<Storage.Models.Rate> GetRates()
        {
            return _rateService.GetRates();
        }

        [UseProjection]
        public IExecutable<Storage.Models.User> GetUsers()
        {
            return _usersService.GetUsers();
        }

        public async Task<double> GetUserRateForFestival(string festivalId, string userId)
        {
            return await _rateService.GetRateForFestivalGivenByUser(festivalId, userId);
        }

        #endregion
    }
}
