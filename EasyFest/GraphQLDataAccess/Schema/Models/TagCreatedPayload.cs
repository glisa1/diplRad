using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class TagCreatedPayload
    {
        public TagCreatedPayload(string mutationId)
        {
            ClientMutationId = mutationId;
        }

        public string ClientMutationId { get; set; }
    }
}
