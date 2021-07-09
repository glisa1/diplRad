using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class UpdateFestivalInput
    {
        public UpdateFestivalInput(string festivalId,
                                    string name,
                                    int month,
                                    int day,
                                    string clientMutationId)
        {
            FestivalId = festivalId;
            Name = name;
            Month = month;
            Day = day;
            ClientMutationId = clientMutationId;
        }

        public string FestivalId { get; set; }

        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public string ClientMutationId { get; set; }
    }
}
