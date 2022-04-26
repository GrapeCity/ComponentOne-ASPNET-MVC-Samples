using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: StickyHeaders
        public ActionResult StickyHeaders()
        {
            return View(Models.FlexGridData.GetSales(100));
        }
    }
}