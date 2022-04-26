using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Tooltips
        public ActionResult Tooltips()
        {
            return View(Models.FlexChartData.GetStocks2());
        }
    }
}