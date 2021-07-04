using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class FestivalLocationsType : ObjectType<Storage.Models.FestivalLocation>
    {
        protected override void Configure(IObjectTypeDescriptor<Storage.Models.FestivalLocation> descriptor)
        {

        }
    }
}
