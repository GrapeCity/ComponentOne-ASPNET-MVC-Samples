using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: RenderCycle
        public ActionResult RenderCycle()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}