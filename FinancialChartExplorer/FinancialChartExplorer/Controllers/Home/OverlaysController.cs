using FinancialChartExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        //
        // GET: /Overlays/
        public ActionResult Overlays()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }
    }
}
