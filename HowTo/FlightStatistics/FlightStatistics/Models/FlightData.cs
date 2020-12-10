using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class FlightDataModel
    {
        public IEnumerable<Airport> AirportsData { get; set; }
        public IEnumerable<Airline> Airlines { get; set; }
        public IEnumerable<AirportMonthlyData> AirportMonthlyData { get; set; }
        public List<AirportsGroupedData> GroupedData { get; set; }
        public string City { get; set; }
    }
}
