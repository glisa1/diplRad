using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.FestivalLocationsService
{
    public interface IFestivalLocationsService
    {
        FestivalLocation GetFestivalLocation(string festivalId);

        Task<FestivalLocation> GetFestivalLocationAsync(string festivalId);

        List<FestivalLocation> GetAllFestivalLocations();

        Task<List<FestivalLocation>> GetAllFestivalLocationsAsync();

        void InsertFestivalLocation(FestivalLocation model);

        Task InsertFestivalLocationAsync(FestivalLocation model);

        void DeleteFestivalLocation(string objectId);

        Task DeleteFestivalLocationAsync(string objectId);
    }
}
