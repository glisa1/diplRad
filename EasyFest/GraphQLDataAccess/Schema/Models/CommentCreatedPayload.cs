using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CommentCreatedPayload
    {
        public CommentCreatedPayload(Comment comment,
                                    string mutationId)
        {
            Comment = comment;
            ClientMutationId = mutationId;
        }

        public Comment Comment { get; set; }

        public string ClientMutationId { get; set; }
    }
}
