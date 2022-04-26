using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: LegendStyles
        public ActionResult LegendStyles()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}