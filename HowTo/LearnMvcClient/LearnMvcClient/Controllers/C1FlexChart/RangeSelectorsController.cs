using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: RangeSelectors
        public ActionResult RangeSelectors()
        {
            return View(Models.FlexChartData.GetStocks1());
        }
    }
}