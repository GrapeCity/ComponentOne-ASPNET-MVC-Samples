using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Axes
        public ActionResult Axes()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}