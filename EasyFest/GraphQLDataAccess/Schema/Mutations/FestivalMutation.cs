using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Storage.Models;
using Storage.Services.CommentsService;
using Storage.Services.FestivalLocationsService;
using Storage.Services.FestivalService;
using Storage.Services.RateService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema.Mutations
{
    public class FestivalMutation
    {
        #region Init

        private readonly IFestivalService _festivalService;
        private readonly IFestivalLocationsService _locationService;
        private readonly IRateService _rateService;
        private readonly ICommentsService _commentService;

        public FestivalMutation(IFestivalService festivalService,
                                IFestivalLocationsService locationsService,
                                IRateService rateService,
                                ICommentsService commentsService)
        {
            _festivalService = festivalService;
            _locationService = locationsService;
            _rateService = rateService;
            _commentService = commentsService;
        }

        #endregion

        #region Methods

        public async Task<FestivalCreatedPayload> CreateFestival(CreateFestivalInput input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name cannot be empty.")
                        .SetCode("NAME_EMPTY")
                        .Build());
            }

            var newFestival = new Festival
            {
                Name = input.Name,
                Day = input.Day,
                Month = input.Month,
                Description = input.Description,
                ImageName = new Guid().ToString()
            };

            await _festivalService.InsertFestivalAsync(newFestival);

            return new FestivalCreatedPayload(newFestival, input.ClientMutationId);
        }

        public async Task<FestivalCreatedPayload> UpdateFestivalAsync(UpdateFestivalInput input)
        {
            if (string.IsNullOrEmpty(input.Name))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Name cannot be empty.")
                        .SetCode("NAME_EMPTY")
                        .Build());
            }

            var newFestival = new Festival
            {
                Name = input.Name,
                Day = input.Day,
                Month = input.Month
            };

            await _festivalService.InsertFestivalAsync(newFestival);

            return new FestivalCreatedPayload(newFestival, input.ClientMutationId);
        }

        /// <summary>
        /// Deletes festival and all adjacent data.
        /// </summary>
        /// <param name="festivalId"></param>
        /// <returns></returns>
        public async Task DeleteFestivalAsync(string festivalId)
        {
            //We call each service that deletes adjacent data.
            await _locationService.DeleteFestivalLocationByFestivalIdAsync(festivalId);
            await _rateService.DeleteRatesByFestivalidAsync(festivalId);
            await _commentService.DeleteCommentsByFestivalIdAsync(festivalId);
        }

        #endregion
    }
}
