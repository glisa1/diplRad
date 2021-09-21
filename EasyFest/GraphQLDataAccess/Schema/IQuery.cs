using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema
{
    public interface IQuery
    {
        //IExecutable<Storage.Models.Festival> GetFestivals();
        //IEnumerable<Storage.Models.Festival> GetFestivals();
        IQueryable<Storage.Models.Festival> GetFestivals();

        IExecutable<Storage.Models.Festival> GetFestivalsInfo();

        //IExecutable<Storage.Models.FestivalLocation> GetFestivalLocationsInfo();

        //IExecutable<Storage.Models.Festival> GetFestivalById(string id);

        Task<Storage.Models.Festival> GetFestivalById(string id);

        //IExecutable<Storage.Models.FestivalLocation> GetFestivalLocations();
        //IQueryable<Storage.Models.FestivalLocation> GetFestivalLocations();

        IExecutable<Storage.Models.Comment> GetComments();

        IExecutable<Storage.Models.Rate> GetRates();

        IExecutable<Storage.Models.User> GetUsers();

        IQueryable<Storage.Models.User> GetUsersFilter();

        IExecutable<Storage.Models.Tag> GetTags();
    }
}
