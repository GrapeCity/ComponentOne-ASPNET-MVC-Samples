using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesOriginPosition
        public ActionResult AxesOriginPosition()
        {
            ViewBag.Points1 = Models.FlexChartData.GetPoints(50, 0, 3);
            ViewBag.Points2 = Models.FlexChartData.GetPoints(40, 100, 12);
            ViewBag.Points3 = Models.FlexChartData.GetPoints(30, -100, 24);
            return View();
        }
    }
}