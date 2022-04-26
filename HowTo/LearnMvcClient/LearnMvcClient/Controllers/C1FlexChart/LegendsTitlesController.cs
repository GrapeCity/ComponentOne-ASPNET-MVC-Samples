using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: LegendsTitles
        public ActionResult LegendsTitles()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}