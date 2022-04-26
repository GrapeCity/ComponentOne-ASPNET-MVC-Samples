using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Series
        public ActionResult Series()
        {
            return View(Models.FlexChartData.GetSummaries());
        }
    }
}