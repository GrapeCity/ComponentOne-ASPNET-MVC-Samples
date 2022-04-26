using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Filtering
        public ActionResult Filtering()
        {
            return View(Models.FlexChartData.GetStocks1());
        }
    }
}