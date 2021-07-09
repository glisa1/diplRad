using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateRateInput
    {
        public CreateRateInput(double rateValue,
                            string festivalId,
                            string userId,
                            string clientMutationId)
        {
            RateValue = rateValue;
            FestivalId = festivalId;
            UserId = userId;
            ClientMutationId = clientMutationId;
        }

        public double RateValue { get; set; }

        public string FestivalId { get; set; }

        public string UserId { get; set; }

        //public Festival Festival { get; set; }

        //public User User { get; set; }

        public string ClientMutationId { get; set; }
    }
}
