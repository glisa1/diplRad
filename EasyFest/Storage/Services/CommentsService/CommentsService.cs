using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.CommentsService
{
    public class CommentsService : ICommentsService
    {
        #region Init

        //private readonly IMongoDbConnectService _dbConnection;
        private readonly IMongoCollection<Comment> _comments;

        public CommentsService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            //_dbConnection = db;
            _comments = db.database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        #endregion

        #region Methods

        public IExecutable<Comment> GetComments() => _comments.AsExecutable();

        public IExecutable<Comment> GetCommentsForFestival(string festivalId) => _comments.Find(x => x.FestivalId == festivalId).AsExecutable();

        public async Task<Comment> GetCommentById(string commentId) => await _comments.Find(x => x.Id == commentId).FirstOrDefaultAsync();

        public List<Comment> GetAllCommentsForFestival(string festivalId) => _comments.Find(x => x.Festival.Id == festivalId).ToList();

        public async Task<int> GetNumberOfCommentsForFestival(string festivalId)
        {
            var filter = Builders<Comment>.Filter.Eq(s => s.FestivalId, festivalId);

            return (int)await _comments.CountDocumentsAsync(filter);
        }

        public async Task<List<Comment>> GetAllCommentsForFestivalAsync(string festivalId)
            => await _comments.Find(x => x.Festival.Id == festivalId).ToListAsync();

        public void InsertComment(Comment model) => _comments.InsertOne(model);

        public async Task InsertCommentAsync(Comment model) => await _comments.InsertOneAsync(model);

        public void DeleteComment(string commentId) => _comments.DeleteOne(x => x.Id == commentId);

        public async Task DeleteCommentAsync(string commentId) => await _comments.DeleteOneAsync(x => x.Id == commentId);

        public async Task DeleteCommentsByFestivalIdAsync(string festivalId) => await _comments.DeleteManyAsync(x => x.FestivalId == festivalId);

        public async Task UpdateCommentAsync(Comment model)
        {
            var filter = Builders<Comment>.Filter.Eq(s => s.Id, model.Id);
            var update = Builders<Comment>.Update.Set(s => s.CommentBody, model.CommentBody);

            var res = await _comments.UpdateOneAsync(filter, update);
        }

        public async Task<int> GetNumberOfCommentsPostedByUser(string userId)
        {
            return (int)await _comments.Find(x => x.UserId == userId).CountDocumentsAsync();
        }

        #endregion
    }
}
