using HotChocolate.Types;
using Storage.Services.FestivalService;
using Storage.Services.UserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class RateType : ObjectType<Storage.Models.Rate>
    {
        #region

        private readonly IFestivalService _festivalService;
        private readonly IUserService _usersService;

        public RateType(IFestivalService festivalService,
                        IUserService userService)
        {
            _festivalService = festivalService;
            _usersService = userService;
        }

        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.Rate> descriptor)
        {
            descriptor.Description("Rates given to festivals.");
            descriptor.Field(f => f.Id)
                .Description("Identificator")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.RateValue)
                .Description("Rate given.")
                .Type<NonNullType<DecimalType>>();
            descriptor.Field(f => f.FestivalId)
                .Description("Festival identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Festival)
                .Description("Festival.")
                .Type<FestivalType>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Rate>();
                    return await _festivalService.GetFestivalByIdAsync(initiative.FestivalId);
                });
            descriptor.Field(f => f.UserId)
                .Description("User identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.User)
                .Description("User.")
                .Type<UserType>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Rate>();
                    return await _usersService.GetUserWithIdAsync(initiative.UserId);
                });
        }
    }
}
