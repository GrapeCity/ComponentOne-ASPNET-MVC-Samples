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
        public ActionResult EventAnnotations()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { };
            ViewBag.Tooltips = BoxData.GetAnnotationTooltips();
            ViewBag.ChartType = C1.Web.Mvc.Finance.ChartType.Candlestick;
            return View(model);
        }
    }
}
