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
                                    string mutationId)
        {
            Name = name;
            Month = month;
            Day = day;
            ClientMutationId = mutationId;
        }

        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public string ClientMutationId { get; set; }
    }
}
