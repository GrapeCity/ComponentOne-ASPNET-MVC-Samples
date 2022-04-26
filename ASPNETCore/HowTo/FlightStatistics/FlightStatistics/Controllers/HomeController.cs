using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FlightStatistics.Models;
using Microsoft.EntityFrameworkCore;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace FlightStatistics.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirportEntities _db;

        public HomeController(AirportEntities db)
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

            ViewBag.Years = FlightStatistics.Models.Data.GetYears();

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
