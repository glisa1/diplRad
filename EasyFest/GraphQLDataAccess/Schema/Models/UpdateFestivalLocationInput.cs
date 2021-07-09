using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class UpdateFestivalLocationInput
    {
        public UpdateFestivalLocationInput(string festivalLocationId,
                                            double longitude,
                                            double latitude,
                                            string address,
                                            string city,
                                            string state,
                                            string mutationId)
        {
            FestivalLocationId = festivalLocationId;
            Longitude = longitude;
            Latitude = latitude;
            Address = address;
            City = city;
            State = state;
            ClientMutationId = mutationId;
        }

        public string FestivalLocationId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ClientMutationId { get; set; }
    }
}
