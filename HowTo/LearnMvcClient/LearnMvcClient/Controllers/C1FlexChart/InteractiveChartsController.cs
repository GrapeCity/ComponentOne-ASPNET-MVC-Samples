using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: InteractiveCharts
        public ActionResult InteractiveCharts()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}