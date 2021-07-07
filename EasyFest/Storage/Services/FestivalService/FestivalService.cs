using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.FestivalService
{
    public class FestivalService : IFestivalService
    {
        #region Init

        //private readonly IMongoDbConnectService _dbConnection;
        private readonly IMongoCollection<Festival> _festivals;

        //public IMongoCollection<Festival> Festivals { get { return _festivals; } }

        public FestivalService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            //_dbConnection = db;
            _festivals = db.database.GetCollection<Festival>(settings.FestivalCollectionName);
        }

        #endregion

        #region Methods

        public List<Festival> GetAllFestivals() => _festivals.Find(_ => true).ToList();

        public IExecutable<Festival> GetFestivals() => _festivals.AsExecutable();

        public IExecutable<Festival> GetFestivalById(string id) => _festivals.Find(x => x.Id == id).AsExecutable();

        public async Task<Festival> GetFestivalByIdAsync(string festivalId) => await _festivals.Find(x => x.Id == festivalId).FirstOrDefaultAsync();

        public Festival GetFestival(string objectId) => _festivals.Find<Festival>(x => x.Id == objectId).FirstOrDefault();

        public void InsertFestival(Festival model) => _festivals.InsertOne(model);

        public async Task InsertFestivalAsync(Festival model) => await _festivals.InsertOneAsync(model);

        public void DeleteFestival(string objectId) => _festivals.DeleteOne(x => x.Id == objectId);

        #endregion
    }
}
