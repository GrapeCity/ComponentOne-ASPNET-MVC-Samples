using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public class FlightStatisticsController : Controller
    {
        private readonly AirportEntities _db;

        public FlightStatisticsController(AirportEntities db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var airportsData = _db.Airports.ToList();
            var airportsMonthlyData = _db.AirportsMonthlyData.ToList();
            var airportGroupedData = Data.GetGroupedData(airportsData);

            FlightDataModel fdm = new FlightDataModel
            {
                AirportsData = Data.GetAirports(airportsData, airportsMonthlyData),
                AirportMonthlyData = airportsMonthlyData,
                GroupedData = airportGroupedData
            };

            ViewBag.Years = Data.GetYears();

            ViewBag.AirportMonthlyData = airportsMonthlyData;
            ViewBag.GroupedData = airportGroupedData;
            return View(fdm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
