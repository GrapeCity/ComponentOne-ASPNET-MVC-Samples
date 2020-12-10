using FinancialChartExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult HeikinAshi()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel();
            ViewBag.ChartType = C1.Web.Mvc.Finance.ChartType.HeikinAshi;
            return View(model);
        }
    }
}
