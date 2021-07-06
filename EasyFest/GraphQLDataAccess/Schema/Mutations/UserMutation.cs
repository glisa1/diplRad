using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using Storage.Models;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema.Mutations
{
    public class UserMutation
    {
        #region Init

        private readonly IUserService _userService;

        public UserMutation(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<UserCreatedPayload> AddUser(CreateUserInput input)
        {
            if (string.IsNullOrEmpty(input.Username))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Email))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email cannot be empty.")
                        .SetCode("EMAIL_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password cannot be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            string salt = Guid.NewGuid().ToString("N");

            byte[] hash;
            using (var sha = SHA512.Create())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + salt));
            }

            User model = new User
            {
                Email = input.Email,
                Password = Convert.ToBase64String(hash),
                Salt = salt,
                Username = input.Username
            };

            await _userService.AddUserAsync(model);

            return new UserCreatedPayload(model, input.ClientMutationId);
        }
    }
}
