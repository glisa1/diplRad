﻿using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //public IExecutable<Festival> GetFestivals() => _festivals.AsExecutable();

        //public IEnumerable<Festival> GetFestivals() => _festivals.AsQueryable();

        public IQueryable<Festival> GetFestivals() => _festivals.AsQueryable();

        public IExecutable<Festival> GetFestivalsExec() => _festivals.AsExecutable();

        public IExecutable<Festival> GetFestivalById(string id) => _festivals.Find(x => x.Id == id).AsExecutable();

        public async Task<Festival> GetFestivalByIdAsync(string festivalId) => (Festival)await _festivals.Find(x => x.Id == festivalId).AsExecutable().FirstOrDefaultAsync(new System.Threading.CancellationToken());

        public Festival GetFestival(string objectId) => _festivals.Find<Festival>(x => x.Id == objectId).FirstOrDefault();

        public void InsertFestival(Festival model) => _festivals.InsertOne(model);

        public async Task InsertFestivalAsync(Festival model) => await _festivals.InsertOneAsync(model);

        public async Task<bool> GetFestivalByNameAsync(string festivalName)
        {
            var count = await _festivals.Find(x => x.Name == festivalName).CountDocumentsAsync();
            if (count > 0)
                return true;

            return false;
        }

        public async Task<string> GetFestivalIdByNameAsync(string festivalName)
        {
            var foundFest = await _festivals.Find(x => x.Name == festivalName).FirstOrDefaultAsync();
            if (foundFest != null)
            {
                return foundFest.Id;
            }

            return null;
        }

        public async Task UpdateFestivalAsync(Festival model)
        {
            var filter = Builders<Festival>.Filter.Eq(s => s.Id, model.Id);
            var update = Builders<Festival>.Update
                .Set(s => s.Name, model.Name)
                .Set(s => s.Description, model.Description)
                .Set(s => s.Day, model.Day)
                .Set(s => s.EndDay, model.EndDay)
                .Set(s => s.EndMonth, model.EndMonth)
                .Set(s => s.Month, model.Month);

            await _festivals.UpdateOneAsync(filter, update);
        }

        public void DeleteFestival(string objectId) => _festivals.DeleteOne(x => x.Id == objectId);

        public async Task DeleteFestivalAsync(string festivalId) => await _festivals.DeleteOneAsync(x => x.Id == festivalId);

        #endregion
    }
}
