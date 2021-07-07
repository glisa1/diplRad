using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using Storage.Models;
using Storage.Services.FestivalLocationsService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema.Mutations
{
    public class FestivalLocationMutation
    {
        #region Init

        private readonly IFestivalLocationsService _festivalLocationsService;

        public FestivalLocationMutation(IFestivalLocationsService festivalLocationsService)
        {
            _festivalLocationsService = festivalLocationsService;
        }

        #endregion

        public async Task<FestivalLocationCreatedPayload> CreateFestivalLocation(CreateFestivalLocationInput input)
        {
            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            var newFestivalLocation = new FestivalLocation
            {
                Address = input.Address,
                City = input.City,
                Festival = input.Festival,
                FestivalId = input.FestivalId,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                State = input.State
            };

            await _festivalLocationsService.InsertFestivalLocationAsync(newFestivalLocation);

            return new FestivalLocationCreatedPayload(newFestivalLocation, input.ClientMutationId);
        }
    }
}
