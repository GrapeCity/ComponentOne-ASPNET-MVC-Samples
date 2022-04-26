using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CustomGlyphs
        public ActionResult CustomGlyphs()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}