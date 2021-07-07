using GraphQLDataAccess.Schema.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class FestivalMutationType : ObjectType<FestivalMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<FestivalMutation> descriptor)
        {
            descriptor.Description("Used to create or update festivals.");
            descriptor.Field(f => f.CreateFestival(default))
                .Description("Creates festival.");
        }
    }
}
