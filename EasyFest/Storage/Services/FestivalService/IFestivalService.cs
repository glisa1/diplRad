using HotChocolate;
using MongoDB.Driver;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Services.FestivalService
{
    public interface IFestivalService
    {

        //IMongoCollection<Festival> Festivals { get; }

        List<Festival> GetAllFestivals();

        Festival GetFestival(string objectId);

        void InsertFestival(Festival model);

        void DeleteFestival(string objectId);

        IExecutable<Festival> GetFestivals();

        IExecutable<Festival> GetFestivalById(string id);
    }
}
