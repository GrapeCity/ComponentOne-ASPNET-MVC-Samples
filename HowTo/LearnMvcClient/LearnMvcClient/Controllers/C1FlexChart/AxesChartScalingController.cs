using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesChartScaling
        public ActionResult AxesChartScaling()
        {
            return View(Models.PopulationGdp.GetData());
        }
    }
}