using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Storage.Models;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Init

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor,
                                        IUserService userService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        #endregion

        #region Methods


        public async Task SignInAsync(User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Username))
                claims.Add(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, FestAuthenticationDefaults.ClaimsIssuer));

            //if (!string.IsNullOrEmpty(user.Email))
            //    claims.Add(new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, FestAuthenticationDefaults.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, FestAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(FestAuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);


            //cache authenticated customer
            //_cachedCustomer = customer;
        }

        public async Task SignOutAsync()
        {
            //reset cached customer
            //_cachedCustomer = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(FestAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<User> GetAuthenticatedCustomer()
        {
            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(FestAuthenticationDefaults.AuthenticationScheme).Result;
            if (!authenticateResult.Succeeded)
                return null;

            User user = null;

            //try to get user by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                && claim.Issuer.Equals(FestAuthenticationDefaults.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (usernameClaim != null)
                user = await _userService.GetUserWithUsernameAsync(usernameClaim.Value);

            //whether the found user is available
            if (user == null)
                return null;

            return user;
        }

        #endregion 
    }
}
