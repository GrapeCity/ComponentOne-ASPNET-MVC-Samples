using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Sorting
        public ActionResult Sorting()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}