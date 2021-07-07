using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateCommentInput
    {
        public CreateCommentInput(string userId, 
                                string comment, 
                                string festivalId,
                                User user,
                                Festival festival,
                                string mutationId)
        {
            UserId = userId;
            FestivalId = festivalId;
            CommentBody = comment;
            User = user;
            Festival = festival;
            ClientMutationId = mutationId;
        }

        public string CommentBody { get; set; }

        public string UserId { get; set; }

        public string FestivalId { get; set; }

        public User User { get; set; }

        public Festival Festival { get; set; }

        public string ClientMutationId { get; set; }
    }
}
