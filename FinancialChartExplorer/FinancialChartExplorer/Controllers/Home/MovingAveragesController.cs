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
        public ActionResult MovingAverages()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }
    }
}
