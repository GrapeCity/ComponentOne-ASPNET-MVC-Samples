using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FinancialChartController : Controller
    {
        public ActionResult EquiVolume()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel() { };
            ViewBag.ChartType = C1.Web.Mvc.Finance.ChartType.EquiVolume;
            return View(model);
        }
    }
}
