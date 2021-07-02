using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using Storage.Services.FestivalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLDataAccess.Schema
{
    public class Query
    {
        public Festival GetFestival() =>
            new Festival
            {
                Name = "TestName"
            };

        public Storage.Models.Festival GetFestival([Service] IFestivalService festivalService, string festivalId)
        {
            return festivalService.GetFestival(festivalId);
        }
    }
}
