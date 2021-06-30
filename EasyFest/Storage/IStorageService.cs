using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public interface IStorageService
    {
        #region Festivals

        List<Festival> GetAllFestivals();

        Festival GetFestival(string objectId);

        void InsertFestival(Festival model);

        void DeleteFestival(string objectId);

        #endregion

        #region Festival Locations

        FestivalLocation GetFestivalLocation(string festivalId);

        Task<FestivalLocation> GetFestivalLocationAsync(string festivalId);

        List<FestivalLocation> GetAllFestivalLocations();

        Task<List<FestivalLocation>> GetAllFestivalLocationsAsync();

        void InsertFestivalLocation(FestivalLocation model);

        Task InsertFestivalLocationAsync(FestivalLocation model);

        void DeleteFestivalLocation(string objectId);

        Task DeleteFestivalLocationAsync(string objectId);

        #endregion

        #region Comments

        List<Comments> GetAllCommentsForFestival(string festivalId);

        Task<List<Comments>> GetAllCommentsForFestivalAsync(string festivalId);

        #endregion
    }
}
