using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AnnotationsZonesRedux
        public ActionResult AnnotationsZonesRedux()
        {
            return View(Models.FlexChartData.GetStocks3());
        }
    }
}