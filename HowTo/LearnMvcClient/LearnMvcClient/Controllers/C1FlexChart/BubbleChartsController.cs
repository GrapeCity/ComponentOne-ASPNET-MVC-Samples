using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: BubbleCharts
        public ActionResult BubbleCharts()
        {
            return View(Models.FlexChartData.GetSales3());
        }
    }
}