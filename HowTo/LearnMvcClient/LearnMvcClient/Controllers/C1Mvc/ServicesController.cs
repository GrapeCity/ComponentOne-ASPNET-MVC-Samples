using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Services
        public ActionResult Services()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}