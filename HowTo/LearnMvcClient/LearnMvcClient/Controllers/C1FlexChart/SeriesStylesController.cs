using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: SeriesStyles
        public ActionResult SeriesStyles()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}