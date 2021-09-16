using HotChocolate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.TagService
{
    public interface ITagService
    {
        IExecutable<Storage.Models.Tag> GetTags();

        Task InsertTagAsync(Storage.Models.Tag model);

        IExecutable<Storage.Models.Tag> GetTagById(string id);

        Task DeleteTagAsync(string id);

        Task<Storage.Models.Tag> GetTagByNameAsync(string name);

        IExecutable<Storage.Models.Tag> GetTagsByIdList(List<string> ids);

        Task DeleteTag(string tagId);
    }
}
