using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesZoom
        public ActionResult AxesZoom()
        {
            return View(Models.FlexChartData.GetStocks1());
        }
    }
}