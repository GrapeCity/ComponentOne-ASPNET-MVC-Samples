using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialChartExplorer.Models;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult PointAndFigure()
        {
            ViewBag.FbData = FbData.GetDataFromJson();
            ViewBag.RsData = FbData.GetReativeStrengthDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { Settings = CreatePointAndFigureSettings(), DefaultValues = CreatePointAndFigureDefaultValues() };
            return View();
        }

        private static IDictionary<string, object[]> CreatePointAndFigureSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Fields", new object[]{"HighLow","Close"}},
                {"Reversal", new object[]{"2","3","4","5"}},
                {"Scaling", new object[]{"Traditional", "Fixed", "Dynamic"}},
                {"BoxSize", new object[]{"1","2","3","4","5"}},
                {"Period", new object[]{"10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30"}},
                {"Stroke", new object[]{"LightBlue","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}},
                {"AltStroke", new object[]{"LightBlue","Red", "Green", "Blue","Black","Purple","Yellow","Orange","Silver","Brown"}},
            };

            return settings;
        }

        private static IDictionary<string, object> CreatePointAndFigureDefaultValues()
        {
            return new Dictionary<string, object>
            {
                { "Reversal", "3" },
                { "Period", "20" },
                { "Stroke", "Black" },
                { "AltStroke", "Red" }
            };
        }
    }
}
