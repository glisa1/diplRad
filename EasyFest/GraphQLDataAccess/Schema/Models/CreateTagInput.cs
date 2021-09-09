using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateTagInput
    {
        public CreateTagInput(string tagName,
                            string tagColor,
                            string clientMutationId)
        {
            TagName = tagName;
            TagColor = tagColor;
            ClientMutationId = clientMutationId;
        }

        public string TagName { get; set; }

        public string TagColor { get; set; }

        public string ClientMutationId { get; set; }

    }
}
