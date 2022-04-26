using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: SeriesPicker
        public ActionResult SeriesPicker()
        {
            return View(Models.FlexChartData.GetSummaries());
        }
    }
}