using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: ChartTypes
        public ActionResult ChartTypes()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}