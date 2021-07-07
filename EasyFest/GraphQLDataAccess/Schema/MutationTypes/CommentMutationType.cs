using GraphQLDataAccess.Schema.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class CommentMutationType : ObjectType<CommentMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<CommentMutation> descriptor)
        {
            descriptor.Description("Mutation used to create or update comments.");
            descriptor.Field(f => f.AddComment(default))
                .Description("Create comment.");
        }
    }
}
