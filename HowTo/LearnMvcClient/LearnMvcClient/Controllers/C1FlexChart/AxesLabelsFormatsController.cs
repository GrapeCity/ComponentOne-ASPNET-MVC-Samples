using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: AxesLabelsFormats
        public ActionResult AxesLabelsFormats()
        {
            return View(Models.FlexChartData.GetSales2());
        }
    }
}