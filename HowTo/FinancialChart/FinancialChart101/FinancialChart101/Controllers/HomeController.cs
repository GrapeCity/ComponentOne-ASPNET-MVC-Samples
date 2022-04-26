using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialChart101.Models;

namespace FinancialChart101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FinancialChartModel Model = new FinancialChartModel();
            Model.Settings = CreateSettings();
            return View(Model);
        }

        private IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Type", new object[]{"Simple","Exponential","Triangular","Weighted"}}
            };
            return settings;
        }
    }
}