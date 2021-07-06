using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateUserInput
    {
        public CreateUserInput(string clientMutationId,
                                string username,
                                string password,
                                string email)
        {
            this.ClientMutationId = clientMutationId;
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ClientMutationId { get; set; }
    }
}
