using GraphQLDataAccess.Schema.Models;
using System;
using System.Collections.Generic;
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
    }
}
