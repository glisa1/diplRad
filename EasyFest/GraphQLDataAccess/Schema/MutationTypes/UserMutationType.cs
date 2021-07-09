using GraphQLDataAccess.Schema.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class UserMutationType : ObjectType<UserMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<UserMutation> descriptor)
        {
            descriptor.Description("Mutation used to create or update users.");
            descriptor.Field(f => f.AddUser(default))
                .Description("Adds new user.");
            descriptor.Field(f => f.LoginUser(default))
                .Description("Logs user to application.");
            descriptor.Field(f => f.DeleteUser(default))
                .Description("Deletes user.");
        }
    }
}
