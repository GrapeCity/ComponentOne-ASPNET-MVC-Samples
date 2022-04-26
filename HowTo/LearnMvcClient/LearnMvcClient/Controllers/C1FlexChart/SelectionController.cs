using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Selection
        public ActionResult Selection()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}