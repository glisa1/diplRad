using HotChocolate.Types;
using Storage.Services.CommentsService;
using Storage.Services.RateService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class FestivalType : ObjectType<Storage.Models.Festival>
    {
        #region Init

        //private readonly IFestivalService _festivalService;
        private readonly ICommentsService _commentsService;
        private readonly IRateService _rateService;
        //private readonly IUserService _usersService;

        public FestivalType(ICommentsService comentsService,
                                IRateService rateService)
                                //IUserService usersService)
        {
            _commentsService = comentsService;
            _rateService = rateService;
        }

        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.Festival> descriptor)
        {
            descriptor.Description("General informations about festivals.");
            descriptor.Field(f => f.Id)
                .Description("Identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Name)
                .Description("Name of festival.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Month)
                .Description("Month in which festival takes place.")
                .Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Day)
                .Description("Day in which festival takes place.")
                .Type<NonNullType<IntType>>();
            descriptor.Field(f => f.EndDay)
                .Description("Ending day of festival.")
                .Type<NonNullType<IntType>>();
            descriptor.Field(f => f.EndMonth)
                .Description("Ending month of festival.")
                .Type<NonNullType<IntType>>();
            descriptor.Field(f => f.Description)
                .Description("Description for festival.")
                .Type<StringType>();
            descriptor.Field(f => f.Images)
                .Description("Festival images.")
                .Type<ListType<StringType>>();
            descriptor.Field(f => f.Rate)
                .Description("Rate of festival.")
                .Type<NonNullType<FloatType>>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Festival>();
                    return await _rateService.GetRateForFestivalAsync(initiative.Id);
                });
            //descriptor.Field(f => f.RateByCurrentUser)
            //    .Description("Rate given to festival by current user.")
            //    .Type<FloatType>()
            //    .Resolve(async context =>
            //    {
            //        var initiative = context.Parent<Storage.Models.Festival>();
            //        return await _rateService.GetRateForFestivalGivenByUser(initiative.Id);
            //    });
            descriptor.Field(f => f.NumberOfComments)
                .Description("Gets the number of comments for festival.")
                .Type<NonNullType<IntType>>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.Festival>();
                    return await _commentsService.GetNumberOfCommentsForFestival(initiative.Id);
                });
            //descriptor.Field(f => f.FestivalLocation)
            //    .Description("Location of festival.")
            //    .Type<FestivalLocationType>()
            //    //.UseProjection()
            //    .UseFiltering()
            //    .UseSorting();
            descriptor.Field(f => f.CommentsList)
                .Description("Comments written about festival.")
                .Type<ListType<CommentType>>()
                .Resolver(context =>
                {
                    var initiative = context.Parent<Storage.Models.Festival>();
                    return _commentsService.GetCommentsForFestival(initiative.Id);
                });
            descriptor.Field(f => f.RatesList)
                .Description("Rating of festival.")
                .Type<ListType<RateType>>()
                .Resolver(context =>
                {
                    var initiative = context.Parent<Storage.Models.Festival>();
                    return _rateService.GetRatesForFestival(initiative.Id);
                });
        }
    }
}
