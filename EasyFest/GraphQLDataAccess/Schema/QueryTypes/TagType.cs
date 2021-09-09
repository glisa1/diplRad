using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.QueryTypes
{
    public class TagType : ObjectType<Storage.Models.Tag>
    {
        #region Init

        //private readonly IFestivalService _festivalService;
        //private readonly IUserService _usersService;

        //public TagType(IFestivalService festivalService,
        //                    IUserService userService)
        //{
        //    _festivalService = festivalService;
        //    _usersService = userService;
        //}

        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.Tag> descriptor)
        {
            descriptor.Field(t => t.Id)
                .Description("Tags id.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Name)
                .Description("Name of tag.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(t => t.Color)
                .Description("Tags color.")
                .Type<NonNullType<StringType>>();
        }
    }
}
