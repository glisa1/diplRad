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
        private readonly IMongoCollection<Comment> _commentsLocations;

        public CommentsService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            //_dbConnection = db;
            _commentsLocations = db.database.GetCollection<Comment>(settings.CommentsCollectionName);
        }

        #endregion

        #region Methods

        public IExecutable<Comment> GetComments() => _commentsLocations.AsExecutable();

        public List<Comment> GetAllCommentsForFestival(string festivalId) => _commentsLocations.Find(x => x.FestivalId == festivalId).ToList();

        public async Task<List<Comment>> GetAllCommentsForFestivalAsync(string festivalId)
            => await _commentsLocations.Find(x => x.FestivalId == festivalId).ToListAsync();

        public void InsertComment(Comment model) => _commentsLocations.InsertOne(model);

        public async Task InsertCommentAsync(Comment model) => await _commentsLocations.InsertOneAsync(model);

        public void DeleteComment(string commentId) => _commentsLocations.DeleteOne(x => x.Id == commentId);

        public async Task DeleteCommentAsync(string commentId) => await _commentsLocations.DeleteOneAsync(x => x.Id == commentId);

        #endregion
    }
}
