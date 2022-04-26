using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesCustomLabels
        public ActionResult AxesCustomLabels()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}