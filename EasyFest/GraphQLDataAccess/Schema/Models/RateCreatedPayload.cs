using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class RateCreatedPayload
    {
        public RateCreatedPayload(Rate rate,
                                string mutationId)
        {
            Rate = rate;
            ClientMutationId = mutationId;
        }

        public Rate Rate { get; set; }

        public string ClientMutationId { get; set; }
    }
}
