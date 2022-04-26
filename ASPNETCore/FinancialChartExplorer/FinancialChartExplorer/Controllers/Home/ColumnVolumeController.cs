using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult ColumnVolume()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { };
            ViewBag.ChartType = C1.Web.Mvc.Finance.ChartType.ColumnVolume;
            return View(model);
        }
    }
}
