using HotChocolate;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Services.RateService
{
    public interface IRateService
    {
        IExecutable<Rate> GetRates();

        IExecutable<Rate> GetRatesForFestival(string FestivalId);
    }
}
