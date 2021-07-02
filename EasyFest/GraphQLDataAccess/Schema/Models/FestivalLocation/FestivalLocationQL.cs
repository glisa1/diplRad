using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDataAccess.Schema.FestivalLocation
{
    public class FestivalLocationQL
    {
        public string Id { get; set; }

        public string FestivalId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
