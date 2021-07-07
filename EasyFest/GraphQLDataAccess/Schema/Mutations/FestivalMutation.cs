using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using Storage.Models;
using Storage.Services.FestivalService;
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

        public FestivalMutation(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        #endregion

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
                Month = input.Month
            };

            await _festivalService.InsertFestivalAsync(newFestival);

            return new FestivalCreatedPayload(newFestival, input.ClientMutationId);
        }

    }
}
