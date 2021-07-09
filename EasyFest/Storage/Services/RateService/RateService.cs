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

        public async Task DeleteRatesByFestivalidAsync(string festivalId) => await _rates.DeleteManyAsync(x => x.FestivalId == festivalId);

        public async Task UpdateRateAsync(Rate model)
        {
            var filter = Builders<Rate>.Filter.Eq("FestivalId", model.FestivalId) &
                Builders<Rate>.Filter.Eq("UserId", model.UserId);
            var update = Builders<Rate>.Update.Set("RateValue", model.RateValue);

            await _rates.UpdateOneAsync(filter, update);
        }

        /// <summary>
        /// Method returns rate value for festival from rates users gave.
        /// </summary>
        /// <param name="festivalId"></param>
        /// <returns></returns>
        public async Task<double> GetRateForFestivalAsync(string festivalId)
        {
            var result = await _rates.Aggregate()
                    .Match(f => f.FestivalId == festivalId)
                    .Group(b => b.FestivalId, b =>
                        new
                        {
                            FestivalId = b.Key,
                            Rate = b.Average(p => p.RateValue)
                        })
                    .ToListAsync();

            return result.Select(_ => _.Rate).FirstOrDefault();
        }
        #endregion
    }
}
