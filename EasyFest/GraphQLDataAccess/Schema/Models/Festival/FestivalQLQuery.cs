using HotChocolate;
using Storage.Services.FestivalService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Festival
{
    public class FestivalQLQuery
    {
        #region Init

        private readonly IFestivalService _festivalService;

        public FestivalQLQuery(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }

        #endregion

        public IExecutable<Storage.Models.Festival> GetFestivals()
        {
            return _festivalService.GetFestivals();
        }
    }
}
