using System.Collections.Generic;
using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult FibonacciTool()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreateFibonacciToolSettings() };
            return View(model);
        }

        private static IDictionary<string, object[]> CreateFibonacciToolSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Type", new object[]{"Arcs", "Fans", "Retracements", "Time Zones"}},
                {"Uptrend", new object[]{"True","False"}},
                {"Label Position", new object[]{"Bottom", "Center", "Left", "None", "Right", "Top"}},
                {"Range Selector", new object[]{"False","True"}},
                {"Start.X", new object[]{"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                    "11", "12", "13", "14", "15", "20", "25", "30", "35", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "55", "60"}},
                {"Start.Y", new object[]{"15.12", "15.75", "16.12", "16.75", "17.12", "17.75",
                    "18.12", "18.75", "19.12", "19.75", "20.12", "20.75", "21.12", "21.75", "22.12", "22.75", "23.12", "23.75", "24.12", "24.75", "25.12", "25.75"}},
                {"End.X", new object[]{"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
                    "40", "45", "50", "51", "52", "53", "54", "55", "56", "57", "58", "60"}},
                {"End.Y", new object[]{"15.10", "15.53", "16.10", "16.53", "17.10", "17.53",
                    "18.10", "18.53", "19.10", "19.53", "20.10", "20.53", "21.10", "21.53", "22.10", "22.53", "23.10", "23.53", "24.10", "24.53", "25.10", "25.53"}},
                {"StartX", new object[]{"-10", "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4", "5", "10"}},
                {"EndX", new object[]{"-10", "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4", "5", "10"}}
            };

            return settings;
        }

    }
}
