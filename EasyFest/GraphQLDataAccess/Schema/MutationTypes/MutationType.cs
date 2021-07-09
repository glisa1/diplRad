using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.MutationTypes
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field(d => d.AddComment(default));
            descriptor.Field(d => d.AddUser(default));
            descriptor.Field(d => d.CreateFestival(default));
            descriptor.Field(d => d.CreateFestivalLocation(default));
            descriptor.Field(d => d.CreateRate(default));
            descriptor.Field(d => d.DeleteComment(default));
            descriptor.Field(d => d.DeleteFestival(default));
            descriptor.Field(d => d.DeleteFestivalLocation(default));
            descriptor.Field(d => d.DeleteUser(default));
            descriptor.Field(d => d.LoginUser(default));
            descriptor.Field(d => d.UpdateComment(default));
            descriptor.Field(d => d.UpdateFestival(default));
            descriptor.Field(d => d.UpdateFestivalLocation(default));
            descriptor.Field(d => d.UpdateRate(default));
        }
    }
}
