using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Annotations
        public ActionResult Annotations()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}