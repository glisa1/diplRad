using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.FestivalLocationsService
{
    public class FestivalLocationsService : IFestivalLocationsService
    {
        #region Init

        //private readonly IMongoDbConnectService _dbConnection;
        private readonly IMongoCollection<FestivalLocation> _festivalLocations;

        public FestivalLocationsService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            //_dbConnection = db;
            _festivalLocations = db.database.GetCollection<FestivalLocation>(settings.FestivalLocationCollectionName);
        }

        #endregion

        #region Methods

        public FestivalLocation GetFestivalLocation(string festivalId) => _festivalLocations.Find(x => x.FestivalId == festivalId).FirstOrDefault();

        public async Task<FestivalLocation> GetFestivalLocationAsync(string festivalId) => await _festivalLocations.Find(x => x.FestivalId == festivalId).FirstOrDefaultAsync();

        public List<FestivalLocation> GetAllFestivalLocations() => _festivalLocations.Find(_ => true).ToList();

        public async Task<List<FestivalLocation>> GetAllFestivalLocationsAsync() => await _festivalLocations.Find(_ => true).ToListAsync();

        //Za insert treba da se proveri da li vec postoji lokacija, da se ne dupliraju
        public void InsertFestivalLocation(FestivalLocation model) => _festivalLocations.InsertOne(model);

        public async Task InsertFestivalLocationAsync(FestivalLocation model) => await _festivalLocations.InsertOneAsync(model);

        //Ubaciti id za lokaciju festivala, najlakse se tako brise
        public void DeleteFestivalLocation(string objectId) => _festivalLocations.DeleteOne(x => x.Id == objectId);

        public async Task DeleteFestivalLocationAsync(string objectId) => await _festivalLocations.DeleteOneAsync(x => x.Id == objectId);

        #endregion
    }
}
