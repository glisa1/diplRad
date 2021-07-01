using MongoDB.Driver;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Services.MongoDbConnectService
{
    public class MongoDbConnectService : IMongoDbConnectService
    {
        public MongoClient client { get; set; }
        public IMongoDatabase database { get; set; }

        public MongoDbConnectService(IFestDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }
    }
}
