using MongoDB.Driver;
using Storage.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storage
{
    public class StorageService : IStorageService
    {
        #region Init

        private readonly IMongoCollection<Festival> _festivals;
        private readonly IMongoCollection<FestivalLocation> _festivalLocations;

        public StorageService(IFestDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _festivals = database.GetCollection<Festival>(settings.FestivalCollectionName);
            _festivalLocations = database.GetCollection<FestivalLocation>(settings.FestivalLocationCollectionname);
        }

        #endregion

        #region Festivals

        public List<Festival> GetAllFestivals() => _festivals.Find(_ => true).ToList();

        public Festival GetFestival(string objectId) => _festivals.Find<Festival>(x => x.Id == objectId).FirstOrDefault();

        public void InsertFestival(Festival model) => _festivals.InsertOne(model);

        public void DeleteFestival(string objectId) => _festivals.DeleteOne(x => x.Id == objectId);

        #endregion

        #region Festival Locations

        public FestivalLocation GetFestivalLocation(string festivalId) => _festivalLocations.Find(x => x.FestivalId == festivalId).FirstOrDefault();

        public async Task<FestivalLocation> GetFestivalLocationAsync(string festivalId) => await _festivalLocations.Find(x => x.FestivalId == festivalId).FirstOrDefaultAsync();

        //Za insert treba da se proveri da li vec postoji lokacija, da se ne dupliraju
        public void InsertFestivalLocation(FestivalLocation model) => _festivalLocations.InsertOne(model);

        public async Task InsertFestivalLocationAsync(FestivalLocation model) => await _festivalLocations.InsertOneAsync(model);

        //Ubaciti id za lokaciju festivala, najlakse se tako brise
        public void DeleteFestivalLocation(string objectId) => _festivalLocations.DeleteOne(x => x.Id == objectId);

        public async Task DeleteFestivalLocationAsync(string objectId) => await _festivalLocations.DeleteOneAsync(x => x.Id == objectId);

        #endregion
    }
}
