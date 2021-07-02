using HotChocolate;
using Storage.Services.FestivalLocationsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.FestivalLocation
{
    public class FestivalLocationQLQuery
    {
        #region Init

        private readonly IFestivalLocationsService _festivalLocationsService;

        public FestivalLocationQLQuery(IFestivalLocationsService festivalLocationsService)
        {
            _festivalLocationsService = festivalLocationsService;
        }

        #endregion

        public IExecutable<Storage.Models.FestivalLocation> GetFestivalLocations()
        {
            return _festivalLocationsService.GetFestivalLocations();
        }

    }
}
