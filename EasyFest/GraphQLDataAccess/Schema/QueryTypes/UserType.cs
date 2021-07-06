using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class UserType : ObjectType<Storage.Models.User>
    {
        protected override void Configure(IObjectTypeDescriptor<Storage.Models.User> descriptor)
        {
            descriptor.Description("Information about users.");
            descriptor.Field(f => f.Id)
                .Description("User identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Email)
                .Description("Email address of user.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Username)
                .Description("Username of user.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Password)
                .Description("User's account password.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Salt)
                .Description("Password salt.")
                .Type<NonNullType<StringType>>();
        }
    }
}
