using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesRanges
        public ActionResult AxesRanges()
        {
            return View(Models.FlexChartData.GetSales2(150000));
        }
    }
}