using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.UserService
{
    public interface IUserService
    {
        User GetUserWithId(string userId);

        Task<User> GetUserWithIdAsync(string userId);

        void AddUser(User user);

        Task AddUserAsync(User user);

        void DeleteUser(string userId);

        Task DeleteUserAsync(string userId);
    }
}
