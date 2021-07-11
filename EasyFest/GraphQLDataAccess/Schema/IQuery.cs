using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema
{
    public interface IQuery
    {
        IExecutable<Storage.Models.Festival> GetFestivals();

        IExecutable<Storage.Models.Festival> GetFestivalById(string id);

        IExecutable<Storage.Models.FestivalLocation> GetFestivalLocations();

        IExecutable<Storage.Models.Comment> GetComments();

        IExecutable<Storage.Models.Rate> GetRates();

        IExecutable<Storage.Models.User> GetUsers();
    }
}
