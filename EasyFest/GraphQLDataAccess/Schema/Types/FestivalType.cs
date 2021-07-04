using HotChocolate.Types;
using Storage.Services.FestivalLocationsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class FestivalType : ObjectType<Storage.Models.Festival>
    {
        #region Init

        //private readonly IFestivalService _festivalService;
        private readonly IFestivalLocationsService _festivalLocationsService;
        //private readonly ICommentsService _commentsService;
        //private readonly IRateService _rateService;
        //private readonly IUserService _usersService;

        public FestivalType(IFestivalLocationsService locationsService)
                                //ICommentsService festivalLocationsService,
                                //IRateService rateService,
                                //IUserService usersService)
        {
            _festivalLocationsService = locationsService;
        }

        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.Festival> descriptor)
        {
            descriptor.Field(f => f.Id).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Name).Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Rate).Type<NonNullType<FloatType>>();
            descriptor.Field(f => f.Month).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Day).Type<NonNullType<IntType>>();
            descriptor.Field(f => f.FestivalLocationsList)
                .Type<ListType<NonNullType<FestivalLocationsType>>>()
                .Resolver(context =>
                {
                    var initiative = context.Parent<Storage.Models.Festival>();
                    return _festivalLocationsService.GetFestivalLocationsForFestival(initiative.Id);
                });
        }
    }
}
