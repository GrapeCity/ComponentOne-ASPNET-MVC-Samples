using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class Airport
    {
        public string AirportCode { get; set; }
        public string AirportCity { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int OnTimeRanking { get; set; }
        public int Flights { get; set; }
        //public double Tracked { get; set; }
        public double CompletionFactor { get; set; }
        public double OnTime
        {
            get
            {
                return (1 - Delayed);
            }
        }
        public double Delayed { get; set; }
        public double AverageDelay { get; set; }
        public string Region { get; set; }
        public int UserRating { get; set; }

        [NotMapped]
        public IEnumerable<AirportMonthlyData> MonthlyData { get; set; }
    }

    public class AirportMonthlyData
    {
        public int SNo { get; set; }
        public string AirportCode { get; set; }
        public string Region { get; set; }
        public DateTime RecordedDate { get; set; }
        public string RecordedMonth
        {
            get
            {
                return RecordedDate.ToString("MMM");
            }
        }
        public double Delay { get; set; }
    }
}
