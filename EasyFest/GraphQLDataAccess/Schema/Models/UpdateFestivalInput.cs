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
                                    string description,
                                    int endMonth,
                                    int endDay,
                                    List<string> images,
                                    double latitude,
                                    double longitude,
                                    string address,
                                    string city,
                                    string state,
                                    int checkName,
                                    string clientMutationId)
        {
            FestivalId = festivalId;
            Name = name;
            Month = month;
            Day = day;
            EndMonth = endMonth;
            EndDay = endDay;
            Description = description;
            Images = images;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            City = city;
            State = state;
            CheckName = checkName;
            ClientMutationId = clientMutationId;
        }

        public string FestivalId { get; set; }

        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int EndMonth { get; set; }

        public int EndDay { get; set; }

        public string Description { get; set; }

        //public string ImageName { get; set; }

        public List<string> Images { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int CheckName { get; set; }

        public string ClientMutationId { get; set; }
    }
}
