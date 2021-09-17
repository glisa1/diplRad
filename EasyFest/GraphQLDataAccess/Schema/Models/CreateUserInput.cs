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
                                string email,
                                List<string> tags,
                                string imageId)
        {
            this.ClientMutationId = clientMutationId;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Tags = new List<string>(tags[0].Split(','));
            this.ImageId = imageId;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<string> Tags { get; set; }

        public string ImageId { get; set; }

        public string ClientMutationId { get; set; }
    }
}
