using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: CandlestickCharts
        public ActionResult CandlestickCharts()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}