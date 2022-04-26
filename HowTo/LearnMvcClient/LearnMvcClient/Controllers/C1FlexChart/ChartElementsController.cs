using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: ChartElements
        public ActionResult ChartElements()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}