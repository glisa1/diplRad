using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class FestivalCreatedPayload
    {
        public FestivalCreatedPayload(Festival festival,
                                        string mutationId)
        {
            Festival = festival;
            ClientMutationId = mutationId;
        }

        public Festival Festival { get; set; }

        public string ClientMutationId { get; set; }
    }
}
