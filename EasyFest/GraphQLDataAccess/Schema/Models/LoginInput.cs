using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class LoginInput
    {
        public LoginInput(string username,
                            string email,
                            string password,
                            string clientMutationId)
        {
            Username = username;
            Email = email;
            Password = password;
            ClientMutationId = clientMutationId;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ClientMutationId { get; set; }
    }
}
