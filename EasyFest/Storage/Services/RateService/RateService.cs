using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.AuthenticationService;
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
        private readonly IAuthenticationService _authService;

        public RateService(IMongoDbConnectService db, IFestDatabaseSettings settings, IAuthenticationService auth)
        {
            _rates = db.database.GetCollection<Rate>(settings.RateCollectionName);
            _authService = auth;
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

        public async Task<double> GetRateForFestivalGivenByUser(string festivalId, string userId)
        {
            //var user = await _authService.GetAuthenticatedCustomer();
            //if (user != null)
            //{
            //    var rate = await _rates.Find(x => x.FestivalId == festivalId && x.UserId == user.Id).FirstOrDefaultAsync();

            //}

            //return 0;

            var rate = await _rates.Find(x => x.FestivalId == festivalId && x.UserId == userId).FirstOrDefaultAsync();

            if (rate == null)
                return 0;

            return rate.RateValue;
        }

        #endregion
    }
}
