using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexChartController : Controller
    {
        // GET: Gradients
        public ActionResult Gradients()
        {
            return View(Models.FlexChartData.GetSales3());
        }
    }
}