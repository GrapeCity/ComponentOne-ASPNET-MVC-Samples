using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FinancialChartController : Controller
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
