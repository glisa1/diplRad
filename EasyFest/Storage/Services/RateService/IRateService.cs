using HotChocolate;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.RateService
{
    public interface IRateService
    {
        IExecutable<Rate> GetRates();

        IExecutable<Rate> GetRatesForFestival(string FestivalId);

        Task InsertRateAsync(Rate model);

        Task DeleteRatesByFestivalidAsync(string festivalId);

        Task<double> GetRateForFestivalAsync(string festivalId);

        Task UpdateRateAsync(Rate model);

        Task<double> GetRateForFestivalGivenByUser(string festivalId, string userId);
    }
}
