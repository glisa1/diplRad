using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateFestivalInput
    {
        public CreateFestivalInput(string name,
                                    int month,
                                    int day,
                                    string description,
                                    string clientMutationId)
        {
            Name = name;
            Month = month;
            Day = day;
            Description = description;
            ClientMutationId = clientMutationId;
        }

        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public string Description { get; set; }

        public string ClientMutationId { get; set; }
    }
}
