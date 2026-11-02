using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FinancialChartController : Controller
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
