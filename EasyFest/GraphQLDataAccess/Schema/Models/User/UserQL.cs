using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models.User
{
    public class UserQL
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
