using System.Web.Mvc;
using MvcExplorer.Models;


namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult TrendLines()
        {
            var model = Sale.GetData(50);
            return View(model);
        }
    }
}
