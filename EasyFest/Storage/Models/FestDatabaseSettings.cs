using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class FestDatabaseSettings : IFestDatabaseSettings
    {
        public string FestivalCollectionName { get; set; }
        public string FestivalLocationCollectionName { get; set; }
        public string CommentsCollectionName { get; set; }
        public string UserCollectionName { get; set; }
        public string RateCollectionName { get; set; }
        public string TagCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFestDatabaseSettings
    {
        string FestivalCollectionName { get; set; }
        string FestivalLocationCollectionName { get; set; }
        string CommentsCollectionName { get; set; }
        string UserCollectionName { get; set; }
        string RateCollectionName { get; set; }
        string TagCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
