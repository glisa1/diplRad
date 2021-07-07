using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.UserService
{
    public class UserService : IUserService
    {
        #region Init

        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            _users = db.database.GetCollection<User>(settings.UserCollectionName);
        }

        #endregion

        #region Methods

        public IExecutable<User> GetUsers() => _users.AsExecutable();

        public User GetUserWithId(string userId) => _users.Find(x => x.Id == userId).FirstOrDefault();

        public async Task<User> GetUserWithUsernameOrEmailAsync(string usernameOrEmail, bool isUsername)
        {
            if (isUsername)
            {
                return await _users.Find(x => x.Username == usernameOrEmail).FirstOrDefaultAsync();
            }
            else
            {
                return await _users.Find(x => x.Email == usernameOrEmail).FirstOrDefaultAsync();
            }
        }

        public async Task<User> GetUserWithIdAsync(string userId) => await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();

        public void AddUser(User user) => _users.InsertOne(user);

        public async Task AddUserAsync(User user) => await _users.InsertOneAsync(user);

        public void DeleteUser(string userId) => _users.DeleteOne(x => x.Id == userId);

        public async Task DeleteUserAsync(string userId) => await _users.DeleteOneAsync(x => x.Id == userId);

        #endregion
    }
}
