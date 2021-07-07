using GraphQLDataAccess.Schema.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class FestivalLocationMutationType : ObjectType<FestivalLocationMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<FestivalLocationMutation> descriptor)
        {
            descriptor.Description("Used to create or update festival locations.");
            descriptor.Field(f => f.CreateFestivalLocation(default))
                .Description("Creates festival location.");
        }
    }
}
