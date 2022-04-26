using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Index
        public ActionResult Index(int id = 0)
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}