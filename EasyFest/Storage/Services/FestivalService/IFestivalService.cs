using HotChocolate;
using MongoDB.Driver;
using Storage.Models;
using System;
using System.Collections.Generic;
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

        Task InsertFestivalAsync(Festival model);
        IExecutable<Festival> GetFestivals();

        IExecutable<Festival> GetFestivalById(string id);

        Task<Festival> GetFestivalByIdAsync(string festivalId);
    }
}
