using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.RateService
{
    public class RateService : IRateService
    {
        #region Init

        private readonly IMongoCollection<Rate> _rates;

        public RateService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            _rates = db.database.GetCollection<Rate>(settings.RateCollectionName);
        }

        #endregion

        #region Methods

        public IExecutable<Rate> GetRates() => _rates.AsExecutable();

        public IExecutable<Rate> GetRatesForFestival(string FestivalId) => _rates.Find(x => x.FestivalId == FestivalId).AsExecutable();

        public async Task InsertRateAsync(Rate model) => await _rates.InsertOneAsync(model);

        #endregion
    }
}
