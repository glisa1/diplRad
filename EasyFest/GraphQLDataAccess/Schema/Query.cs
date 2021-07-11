using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MongoDB.Driver;
using Storage.Services.CommentsService;
using Storage.Services.FestivalLocationsService;
using Storage.Services.FestivalService;
using Storage.Services.RateService;
using Storage.Services.UserService;
using System.Linq;

namespace GraphQLDataAccess.Schema
{
    public class Query : IQuery
    {
        #region Init

        private readonly IFestivalService _festivalService;
        private readonly IFestivalLocationsService _festivalLocationsService;
        private readonly ICommentsService _commentsService;
        private readonly IRateService _rateService;
        private readonly IUserService _usersService;

        public Query(IFestivalService festivalService,
                                IFestivalLocationsService locationsService,
                                ICommentsService festivalLocationsService,
                                IRateService rateService,
                                IUserService usersService)
        {
            _festivalService = festivalService;
            _festivalLocationsService = locationsService;
            _commentsService = festivalLocationsService;
            _rateService = rateService;
            _usersService = usersService;
        }

        #endregion

        #region Methods

        [UseProjection]
        public IExecutable<Storage.Models.Festival> GetFestivals()
        {
            return _festivalService.GetFestivals();
        }

        [UseProjection]
        public IExecutable<Storage.Models.Festival> GetFestivalById(string id)
        {
            return _festivalService.GetFestivalById(id);
        }

        [UseProjection]
        public IExecutable<Storage.Models.FestivalLocation> GetFestivalLocations()
        {
            return _festivalLocationsService.GetFestivalLocations();
        }

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

        #endregion
    }
}
