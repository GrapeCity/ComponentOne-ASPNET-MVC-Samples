using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AnnotationsZones
        public ActionResult AnnotationsZones()
        {
            return View(Models.FlexChartData.GetStocks3());
        }
    }
}