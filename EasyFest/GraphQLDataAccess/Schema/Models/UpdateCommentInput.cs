using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class UpdateCommentInput
    {
        public UpdateCommentInput(//string userId,
                                string commentId,
                                string commentBody,
                                //string festivalId,
                                //User user,
                                //Festival festival,
                                string clientMutationId)
        {
            //UserId = userId;
            //FestivalId = festivalId;
            CommentBody = commentBody;
            CommentId = commentId;
            //User = user;
            //Festival = festival;
            ClientMutationId = clientMutationId;
        }

        public string CommentId { get; set; }

        public string CommentBody { get; set; }

        //public string UserId { get; set; }
        
        //public string FestivalId { get; set; }

        //public User User { get; set; }

        //public Festival Festival { get; set; }

        public string ClientMutationId { get; set; }
    }
}
