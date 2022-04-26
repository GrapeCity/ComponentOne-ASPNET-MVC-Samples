using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: SpecialChartTypes
        public ActionResult SpecialChartTypes()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}