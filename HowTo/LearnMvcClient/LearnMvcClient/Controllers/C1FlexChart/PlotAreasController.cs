using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: PlotAreas
        public ActionResult PlotAreas()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}