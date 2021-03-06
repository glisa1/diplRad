using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IQueryable<User> GetUsersQ() => _users.AsQueryable(); 

        public User GetUserWithId(string userId) => _users.Find(x => x.Id == userId).FirstOrDefault();

        public async Task<User> GetUserWithUsernameAsync(string username)
        {
            return await _users.Find(x => x.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserWithEmailAsync(string email)
        {
            return await _users.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserWithIdAsync(string userId) => await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();

        public void AddUser(User user) => _users.InsertOne(user);

        public async Task AddUserAsync(User user) => await _users.InsertOneAsync(user);

        public void DeleteUser(string userId) => _users.DeleteOne(x => x.Id == userId);

        public async Task DeleteUserAsync(string userId) => await _users.DeleteOneAsync(x => x.Id == userId);

        public async Task RemoveTagFromUsers(string tagId)
        {
            var usersToUpdate = await _users.FindAsync(x => x.Tags.Contains(tagId));
            var enumTags = usersToUpdate.ToEnumerable();
            foreach (var user in enumTags)
            {
                user.Tags.Remove(tagId);
                var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
                await _users.ReplaceOneAsync(filter, user);
            }
        }

        public async Task AddTagToUser(string tagId, string userId)
        {
            var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            user.Tags.Add(tagId);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task RemoveTagFromUser(string tagId, string userId)
        {
            var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            user.Tags.Remove(tagId);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task FollowFestival(string userId, string festivalId)
        {
            var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            user.SubscribedFests.Add(festivalId);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task UnfollowFestival(string userId, string festivalId)
        {
            var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            var filter = Builders<User>.Filter.Eq(s => s.Id, user.Id);
            user.SubscribedFests.Remove(festivalId);
            await _users.ReplaceOneAsync(filter, user);
        }

        public async Task<bool> CheckIfUserFollows(string userId, string festivalId)
        {
            var user = await _users.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }

            if (user.SubscribedFests.Contains(festivalId))
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
