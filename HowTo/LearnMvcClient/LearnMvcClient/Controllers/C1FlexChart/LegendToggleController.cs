using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: LegendToggle
        public ActionResult LegendToggle()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}