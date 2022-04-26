using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesExtraAxes
        public ActionResult AxesExtraAxes()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}