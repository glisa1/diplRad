using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
