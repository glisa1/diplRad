using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class RateCreatedPayload
    {
        public RateCreatedPayload(Rate rate,
                                Festival festival,
                                string mutationId)
        {
            Rate = rate;
            Festival = festival;
            ClientMutationId = mutationId;
        }

        public Rate Rate { get; set; }

        public Festival Festival { get; set; }

        public string ClientMutationId { get; set; }
    }
}
