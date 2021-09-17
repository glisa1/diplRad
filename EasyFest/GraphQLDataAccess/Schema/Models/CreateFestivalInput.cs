using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace GraphQLDataAccess.Schema.Models
{
    public class CreateFestivalInput
    {
        public CreateFestivalInput(string name,
                                    int month,
                                    int day,
                                    int endMonth,
                                    int endDay,
                                    string description,
                                    List<string> images,
                                    List<string> tags,
                                    double latitude,
                                    double longitude,
                                    string address,
                                    string city,
                                    string state,
                                    string clientMutationId,
                                    int billingDayStart,
                                    int billingMonthStart,
                                    int billingDayEnd,
                                    int billingMonthEnd)
        {
            Name = name;
            Month = month;
            Day = day;
            EndMonth = endMonth;
            EndDay = endDay;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            City = city;
            State = state;
            ClientMutationId = clientMutationId;
            Images = new List<string>(images[0].Split(','));
            Tags = new List<string>(tags[0].Split(','));
            BillingDayStart = billingDayStart;
            BillingMonthStart = billingMonthStart;
            BillingDayEnd = billingDayEnd;
            BillingMonthEnd = billingMonthEnd;
        }

        public string Name { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public int EndMonth { get; set; }

        public int EndDay { get; set; }

        public string Description { get; set; }

        //public string ImageName { get; set; }

        public List<string> Images { get; set; }

        public List<string> Tags { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int BillingDayStart { get; set; }

        public int BillingMonthStart { get; set; }

        public int BillingDayEnd { get; set; }

        public int BillingMonthEnd { get; set; }

        public string ClientMutationId { get; set; }
    }
}
