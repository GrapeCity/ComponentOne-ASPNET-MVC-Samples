using FinancialChartExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        //
        // GET: /Indicators/

        public ActionResult Indicators()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }
    }
}
