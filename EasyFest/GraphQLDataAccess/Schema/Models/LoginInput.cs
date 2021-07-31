using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class LoginInput
    {
        public LoginInput(string username,
                            string password,
                            string clientMutationId)
        {
            Username = username;
            Password = password;
            ClientMutationId = clientMutationId;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        [JsonProperty("clientMutationId")]
        public string ClientMutationId { get; set; }
    }
}
