using HotChocolate.Types;
using Storage.Services.FestivalService;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class CommentType : ObjectType<Storage.Models.Comment>
    {
        #region Init

        private readonly IFestivalService _festivalService;
        private readonly IUserService _usersService;

        public CommentType(IFestivalService festivalService,
                            IUserService userService)
        {
            _festivalService = festivalService;
            _usersService = userService;
        }

        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.Comment> descriptor)
        {
            descriptor.Description("Informations about users comments on festivals.");
            descriptor.Field(f => f.Id)
                .Description("Identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.CommentBody)
                .Description("Content of comment. What user wrote.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.FestivalId)
                .Description("Festival identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Festival)
                .Description("Festival.")
                .Type<NonNullType<FestivalType>>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Comment>();
                    return await _festivalService.GetFestivalByIdAsync(initiative.FestivalId);
                });
            descriptor.Field(f => f.UserId)
                .Description("User identificator")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.User)
                .Description("User.")
                .Type<NonNullType<UserType>>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Comment>();
                    return await _usersService.GetUserWithIdAsync(initiative.UserId);
                });
        }
    }
}
