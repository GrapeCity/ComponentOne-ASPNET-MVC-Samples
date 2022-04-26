using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: ItemFormatter
        public ActionResult ItemFormatter()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}