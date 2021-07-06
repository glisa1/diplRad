using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class UserCreatedPayload
    {
        public UserCreatedPayload(User user, string clientMutationId)
        {
            User = user;
            ClientMutationId = clientMutationId;
        }

        public User User { get; }

        public string ClientMutationId { get; }
    }
}
