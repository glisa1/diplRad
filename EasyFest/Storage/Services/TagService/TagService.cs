using HotChocolate;
using HotChocolate.Data;
using MongoDB.Driver;
using Storage.Models;
using Storage.Services.MongoDbConnectService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.TagService
{
    public class TagService : ITagService
    {
        #region Init

        private readonly IMongoCollection<Storage.Models.Tag> _tags;

        public TagService(IMongoDbConnectService db, IFestDatabaseSettings settings)
        {
            _tags = db.database.GetCollection<Storage.Models.Tag>(settings.TagCollectionName);
        }

        #endregion

        #region Methods

        public IExecutable<Storage.Models.Tag> GetTags() => _tags.AsExecutable();

        public async Task InsertTagAsync(Storage.Models.Tag model) => await _tags.InsertOneAsync(model);

        public IExecutable<Storage.Models.Tag> GetTagById(string id) => _tags.Find(x => x.Id == id).AsExecutable();

        public async Task DeleteTagAsync(string id) => await _tags.DeleteOneAsync(x => x.Id == id);

        public async Task<Storage.Models.Tag> GetTagByNameAsync(string name) => await _tags.Find(x => x.Name == name).FirstOrDefaultAsync();

        public IExecutable<Storage.Models.Tag> GetTagsByIdList(List<string> ids)
        {
            return _tags.Find(x => ids.Contains(x.Id)).AsExecutable();
        }

        public async Task DeleteTag(string tagId)
        {
            await _tags.DeleteOneAsync(t => t.Id == tagId);
        }

        #endregion
    }
}
