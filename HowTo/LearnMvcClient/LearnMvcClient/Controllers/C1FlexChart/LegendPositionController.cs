using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: LegendPosition
        public ActionResult LegendPosition()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}