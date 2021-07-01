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

        List<Comment> GetAllCommentsForFestival(string festivalId);

        Task<List<Comment>> GetAllCommentsForFestivalAsync(string festivalId);

        void InsertComment(Comment model);

        Task InsertCommentAsync(Comment model);

        void DeleteComment(string commentId);

        Task DeleteCommentAsync(string commentId);

        #endregion
    }
}
