using Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateFestivalLocationInput
    {
        public CreateFestivalLocationInput(string festivalId,
                                            //Festival festival,
                                            double latitude,
                                            double longitude,
                                            string state,
                                            string city,
                                            string address,
                                            string clientMutationId)
        {
            FestivalId = festivalId;
            //Festival = festival;
            Latitude = latitude;
            Longitude = longitude;
            State = state;
            City = city;
            Address = address;
            ClientMutationId = clientMutationId;
        }

        public string FestivalId { get; set; }

        //public Festival Festival { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string ClientMutationId { get; set; }
    }
}
