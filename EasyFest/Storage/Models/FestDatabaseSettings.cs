using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Models
{
    public class FestDatabaseSettings : IFestDatabaseSettings
    {
        public string FestivalCollectionName { get; set; }
        public string FestivalLocationCollectionname { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IFestDatabaseSettings
    {
        string FestivalCollectionName { get; set; }
        string FestivalLocationCollectionname { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
