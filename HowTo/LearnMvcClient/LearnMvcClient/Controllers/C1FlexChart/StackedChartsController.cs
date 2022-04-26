using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: StackedCharts
        public ActionResult StackedCharts()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}