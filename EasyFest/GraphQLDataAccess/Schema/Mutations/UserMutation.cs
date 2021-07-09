using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Storage.Models;
using Storage.Services.AuthenticationService;
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
        private readonly IAuthenticationService _authService;

        public UserMutation(IUserService userService,
                            IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authService = authenticationService;
        }

        #endregion

        #region Methods
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

        public async Task<LoginPayload> LoginUser(LoginInput input)
        {
            bool emailEmpty = string.IsNullOrEmpty(input.Email);
            bool usernameEmpty = string.IsNullOrEmpty(input.Username);
            if (emailEmpty && usernameEmpty)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The email or username mustn not be empty.")
                        .SetCode("EMAIL_USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.Password))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The password mustn't be empty.")
                        .SetCode("PASSWORD_EMPTY")
                        .Build());
            }

            string userCredential;
            if (emailEmpty)
                userCredential = input.Username;
            else
                userCredential = input.Email;

            var user = await _userService.GetUserWithUsernameOrEmailAsync(userCredential, emailEmpty);

            if (user is null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            byte[] hash;
            using (var sha = SHA512.Create())
            {
                hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input.Password + user.Salt));
            }

            if (!Convert.ToBase64String(hash).Equals(user.Password, StringComparison.Ordinal))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The specified username or password are invalid.")
                        .SetCode("INVALID_CREDENTIALS")
                        .Build());
            }

            await _authService.SignInAsync(user, true);

            return new LoginPayload(input.ClientMutationId);
        }

        public async Task DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
        }

        #endregion
    }
}
