using GraphQLDataAccess.Schema.Models;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Storage.Models;
using Storage.Services.CommentsService;
using System.Threading.Tasks;

namespace GraphQLDataAccess.Schema.Mutations
{
    public class CommentMutation
    {
        #region Init

        private readonly ICommentsService _commentsService;

        public CommentMutation(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        #endregion

        #region Methods

        public async Task<CommentCreatedPayload> AddComment(CreateCommentInput input)
        {
            if (string.IsNullOrEmpty(input.CommentBody))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var newComment = new Comment
            {
                CommentBody = input.CommentBody,
                UserId = input.UserId,
                FestivalId = input.FestivalId//,
                //Festival = input.Festival,
                //User = input.User
            };

            await _commentsService.InsertCommentAsync(newComment);

            return new CommentCreatedPayload(newComment, input.ClientMutationId);
        }

        public async Task<CommentCreatedPayload> UpdateComment(CreateCommentInput input)
        {
            if (string.IsNullOrEmpty(input.CommentBody))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("Username cannot be empty.")
                        .SetCode("USERNAME_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.FestivalId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The festivalId cannot be empty.")
                        .SetCode("FESTIVALID_EMPTY")
                        .Build());
            }

            if (string.IsNullOrEmpty(input.UserId))
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage("The userId cannot be empty.")
                        .SetCode("USERID_EMPTY")
                        .Build());
            }

            var newComment = new Comment
            {
                CommentBody = input.CommentBody,
                UserId = input.UserId,
                FestivalId = input.FestivalId//,
                //Festival = input.Festival,
                //User = input.User
            };

            await _commentsService.UpdateCommentAsync(newComment);

            return new CommentCreatedPayload(newComment, input.ClientMutationId);
        }

        public async Task DeleteComment(string commentId)
        {
            await _commentsService.DeleteCommentAsync(commentId);
        }

        #endregion
    }
}
