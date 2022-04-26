using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class Airline
    {
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public int Ranking { get; set; }
        public int Flights { get; set; }
        public double Trends { get; set; }
        public double Tracked { get; set; }
        public double OnTime { get; set; }
        public double Delayed { get; set; }
        public double AverageDelay { get; set; }
    }
}
