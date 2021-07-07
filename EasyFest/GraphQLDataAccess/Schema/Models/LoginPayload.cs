using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class LoginPayload
    {
        public LoginPayload(string mutationId)
        {
            ClientMutationId = mutationId;
        }

        public string ClientMutationId { get; set; }
    }
}
