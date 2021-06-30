using MongoDB.Driver;
using Storage.Models;
using System.Collections.Generic;

namespace Storage
{
    public class StorageService : IStorageService
    {
        #region Init

        private readonly IMongoCollection<Festival> _festivals;

        public StorageService(IFestDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _festivals = database.GetCollection<Festival>(settings.FestivalCollectionName);
        }

        #endregion

        #region Festivals

        public List<Festival> GetAllFestivals() => _festivals.Find(_ => true).ToList();

        public Festival GetFestival(string objectId) => _festivals.Find<Festival>(x => x.Id == objectId).FirstOrDefault();

        #endregion
    }
}
