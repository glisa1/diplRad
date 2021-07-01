using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Storage.Services.MongoDbConnectService
{
    public interface IMongoDbConnectService
    {
        MongoClient client { get; set; }

        IMongoDatabase database { get; set; }
    }
}
