using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateRateInput
    {
        public CreateRateInput(double rate,
                            string festivalId,
                            string userId,
                            Festival festival,
                            User user,
                            string mutationId)
        {
            RateValue = rate;
            FestivalId = festivalId;
            UserId = userId;
            Festival = festival;
            User = user;
            ClientMutationId = mutationId;
        }

        public double RateValue { get; set; }

        public string FestivalId { get; set; }

        public string UserId { get; set; }

        public Festival Festival { get; set; }

        public User User { get; set; }

        public string ClientMutationId { get; set; }
    }
}
