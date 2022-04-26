using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: FilterOptimizations
        public ActionResult FilterOptimizations()
        {
            return View(Models.FlexGridData.GetSales(500));
        }
    }
}