using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class FestivalLocationCreatedPayload
    {
        public FestivalLocationCreatedPayload(FestivalLocation festivalLocation,
                                                string mutationId)
        {
            FestivalLocation = festivalLocation;
            ClientMutationId = mutationId;
        }

        public FestivalLocation FestivalLocation { get; set; }

        public string ClientMutationId { get; set; }
    }
}
