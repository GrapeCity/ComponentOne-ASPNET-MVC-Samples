using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: FunnelCharts
        public ActionResult FunnelCharts()
        {
            return View(Models.FlexChartData.GetStages());
        }
    }
}