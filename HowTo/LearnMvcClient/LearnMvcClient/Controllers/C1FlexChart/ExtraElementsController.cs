using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: ExtraElements
        public ActionResult ExtraElements()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}