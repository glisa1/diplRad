using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models.Comment
{
    public class CommentQL
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string CommentBody { get; set; }

        public string FestivalId { get; set; }
    }
}
