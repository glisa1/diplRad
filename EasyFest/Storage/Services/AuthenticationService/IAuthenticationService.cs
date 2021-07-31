using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="isPersistent">Whether the authentication session is persisted across multiple requests</param>
        Task SignInAsync(User customer, bool isPersistent);

        /// <summary>
        /// Sign out
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        Task<User> GetAuthenticatedCustomer();
    }
}
