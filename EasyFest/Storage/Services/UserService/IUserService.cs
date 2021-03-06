using HotChocolate;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.UserService
{
    public interface IUserService
    {

        IExecutable<User> GetUsers();

        IQueryable<User> GetUsersQ();

        User GetUserWithId(string userId);

        Task<User> GetUserWithIdAsync(string userId);

        Task<User> GetUserWithUsernameAsync(string username);

        Task<User> GetUserWithEmailAsync(string email);

        void AddUser(User user);

        Task AddUserAsync(User user);

        void DeleteUser(string userId);

        Task DeleteUserAsync(string userId);

        Task RemoveTagFromUsers(string tagId);

        Task AddTagToUser(string tagId, string userId);

        Task RemoveTagFromUser(string tagId, string userId);

        Task FollowFestival(string userId, string festivalId);

        Task UnfollowFestival(string userId, string festivalId);

        Task<bool> CheckIfUserFollows(string userId, string festivalId);
    }
}
