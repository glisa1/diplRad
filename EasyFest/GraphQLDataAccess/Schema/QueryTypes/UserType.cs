using HotChocolate.Types;
using Storage.Services.CommentsService;
using Storage.Services.RateService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class UserType : ObjectType<Storage.Models.User>
    {
        #region Init

        private readonly ICommentsService _commentsService;
        private readonly IRateService _rateService;

        public UserType(ICommentsService commentsService,
                        IRateService rateService)
        {
            _commentsService = commentsService;
            _rateService = rateService;
        }

        #endregion

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
            descriptor.Field(f => f.IsAdmin)
                .Description("Is user administrator.")
                .Type<BooleanType>();
            descriptor.Field(f => f.CommentsPostedByUser)
                .Description("Number of comments user posted.")
                .Type<IntType>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.User>();
                    return await _commentsService.GetNumberOfCommentsPostedByUser(initiative.Id);
                });
            descriptor.Field(f => f.RatesGivenByUser)
                .Description("Number of ratings given by user.")
                .Type<IntType>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.User>();
                    return await _rateService.GetNumberOfRatesByUser(initiative.Id);
                });
        }
    }
}
