using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Storage.Models;
using Storage.Services.RateService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema.Mutations
{
    public class RateMutation
    {
        #region Init

        private readonly IRateService _rateService;

        public RateMutation(IRateService rateService)
        {
            _rateService = rateService;
        }

        #endregion

        #region Methods

        public async Task<RateCreatedPayload> CreateRate(CreateRateInput input)
        {
            if (input.RateValue < 0 || input.RateValue > 5)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Rate value is incorrect.")
                        .SetCode("BAD_RATEVALUE")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var newRate = new Rate
            {
                RateValue = input.RateValue,
                FestivalId = input.FestivalId,
                UserId = input.UserId//,
                 //Festival = input.Festival,
                 //User = input.User
            };

            await _rateService.InsertRateAsync(newRate);

            return new RateCreatedPayload(newRate, input.ClientMutationId);
        }

        public async Task<RateCreatedPayload> UpdateRate(CreateRateInput input)
        {
            if (input.RateValue < 0 || input.RateValue > 5)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Rate value is incorrect.")
                        .SetCode("BAD_RATEVALUE")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var newRate = new Rate
            {
                RateValue = input.RateValue,
                FestivalId = input.FestivalId,
                UserId = input.UserId//,
                //Festival = input.Festival,
                //User = input.User
            };

            await _rateService.UpdateRateAsync(newRate);

            return new RateCreatedPayload(newRate, input.ClientMutationId);
        }

        #endregion
    }
}
