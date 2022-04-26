using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Palettes
        public ActionResult Palettes()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}