using HotChocolate;
using MongoDB.Driver;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.FestivalService
{
    public interface IFestivalService
    {

        //IMongoCollection<Festival> Festivals { get; }

        List<Festival> GetAllFestivals();

        Festival GetFestival(string objectId);

        void InsertFestival(Festival model);

        void DeleteFestival(string objectId);

        IExecutable<Festival> GetFestivalsExec();

        Task InsertFestivalAsync(Festival model);

        //IExecutable<Festival> GetFestivals();
        //IEnumerable<Festival> GetFestivals();
        IQueryable<Festival> GetFestivals();

        IExecutable<Festival> GetFestivalById(string id);

        Task<Festival> GetFestivalByIdAsync(string festivalId);

        Task UpdateFestivalAsync(Festival model);

        Task<bool> GetFestivalByNameAsync(string festivalName);

        Task<string> GetFestivalIdByNameAsync(string festivalName);

        Task DeleteFestivalAsync(string festivalId);

        Task<List<string>> GetTagIdsListForFestivalByFestivalId(string festivalId);

        Task RemoveTagFromFestivals(string tagId);
    }
}
