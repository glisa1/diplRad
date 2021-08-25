//using HotChocolate.Types;
//using Storage.Services.FestivalService;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace GraphQLDataAccess.Schema.Types
//{
//    public class QueryType : ObjectType<Query>
//    {
//        #region Init

//        private readonly IFestivalService _festivalService;

//        public QueryType(IFestivalService festivalService)
//        {
//            _festivalService = festivalService;
//        }

//        #endregion

//        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
//        {
//            descriptor.Field(f => f.GetFestivals())
//                .UseMongoDbOffsetPaging()
//                .UseFiltering()
//                .Resolve(context =>
//                {
//                    return _festivalService.GetFestivals();
//                });
//        }
//    }
//}
