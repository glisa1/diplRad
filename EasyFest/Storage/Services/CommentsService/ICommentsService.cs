using HotChocolate;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Services.CommentsService
{
    public interface ICommentsService
    {
        IExecutable<Comment> GetComments();

        IExecutable<Comment> GetCommentsForFestival(string festivalId);

        Task<Comment> GetCommentById(string commentId);

        List<Comment> GetAllCommentsForFestival(string festivalId);

        Task<List<Comment>> GetAllCommentsForFestivalAsync(string festivalId);

        Task<int> GetNumberOfCommentsForFestival(string festivalId);

        void InsertComment(Comment model);

        Task InsertCommentAsync(Comment model);

        void DeleteComment(string commentId);

        Task DeleteCommentAsync(string commentId);

        Task DeleteCommentsByFestivalIdAsync(string festivalId);

        Task UpdateCommentAsync(Comment model);

        Task<int> GetNumberOfCommentsPostedByUser(string userId);
    }
}
