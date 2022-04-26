using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: HighLowOpenCloseCharts
        public ActionResult HighLowOpenCloseCharts()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}