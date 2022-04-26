using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: BarCharts
        public ActionResult BarCharts()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}