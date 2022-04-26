using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: WijmoAnimate
        public ActionResult WijmoAnimate()
        {
            return View(Models.FlexGridData.GetSales(100));
        }
    }
}