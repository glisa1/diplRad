using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.CommentsService
{
    public interface ICommentsService
    {
        List<Comment> GetAllCommentsForFestival(string festivalId);

        Task<List<Comment>> GetAllCommentsForFestivalAsync(string festivalId);

        void InsertComment(Comment model);

        Task InsertCommentAsync(Comment model);

        void DeleteComment(string commentId);

        Task DeleteCommentAsync(string commentId);
    }
}
