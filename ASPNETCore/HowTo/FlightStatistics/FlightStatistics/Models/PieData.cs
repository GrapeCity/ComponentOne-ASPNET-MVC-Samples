using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class AirportsGroupedData
    {
        public string Region { get; set; }
        public double AverageDelayAvg { get; set; }
        public double OnTimeAvg { get; set; }
        public int TotalFlights { get; set; }
        public List<Airport> Airports { get; set; }
        public DateTime RecordedDate { get; set; }
    }
}
