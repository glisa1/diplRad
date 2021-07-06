using HotChocolate.Types;
using Storage.Services.FestivalService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Types
{
    public class FestivalLocationType : ObjectType<Storage.Models.FestivalLocation>
    {
        #region Init

        private readonly IFestivalService _festivalService;

        public FestivalLocationType(IFestivalService festivalService)
        {
            _festivalService = festivalService;
        }
        #endregion

        protected override void Configure(IObjectTypeDescriptor<Storage.Models.FestivalLocation> descriptor)
        {
            descriptor.Description("Informations about festivals location.");
            descriptor.Field(f => f.Id)
                .Description("Identificator.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Address)
                .Description("Address of festival.")
                .Type<StringType>();
            descriptor.Field(f => f.City)
                .Description("City of festival.")
                .Type<StringType>();
            descriptor.Field(f => f.State)
                .Description("Country of festival.")
                .Type<StringType>();
            descriptor.Field(f => f.Latitude)
                .Description("Geographical latitude.")
                .Type<FloatType>();
            descriptor.Field(f => f.FestivalId)
                .Description("Identificator of festival.")
                .Type<NonNullType<StringType>>();
            descriptor.Field(f => f.Festival)
                .Description("Festival.")
                .Type<FestivalType>()
                .Resolve(async context =>
                {
                    var initiative = context.Parent<Storage.Models.FestivalLocation>();
                    return await _festivalService.GetFestivalByIdAsync(initiative.FestivalId);
                });
        }
    }
}
