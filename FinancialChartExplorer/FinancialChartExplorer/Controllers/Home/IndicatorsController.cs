using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult Indicators()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }
    }
}
