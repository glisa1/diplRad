using GraphQLDataAccess.Schema.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class RateMutationType : ObjectType<RateMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<RateMutation> descriptor)
        {
            descriptor.Description("Mutation used to create or update rates.");
            descriptor.Field(f => f.CreateRate(default))
                .Description("Creates rate.");
        }
    }
}
