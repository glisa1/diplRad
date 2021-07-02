using HotChocolate;
using Storage.Services.CommentsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models.Comment
{
    public class CommentQLQuery
    {
        #region Init

        private readonly ICommentsService _commentsService;

        public CommentQLQuery(ICommentsService festivalLocationsService)
        {
            _commentsService = festivalLocationsService;
        }

        #endregion

        public IExecutable<Storage.Models.Comment> GetComments()
        {
            return _commentsService.GetComments();
        }

    }
}
