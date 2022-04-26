using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Overlays()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }

    }
}
