using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesGridlinesTickmarks
        public ActionResult AxesGridlinesTickmarks()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}