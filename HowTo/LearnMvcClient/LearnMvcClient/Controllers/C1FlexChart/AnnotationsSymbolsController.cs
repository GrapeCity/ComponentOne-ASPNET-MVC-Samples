using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AnnotationsSymbols
        public ActionResult AnnotationsSymbols()
        {
            return View(Models.FlexChartData.GetStocks3());
        }
    }
}