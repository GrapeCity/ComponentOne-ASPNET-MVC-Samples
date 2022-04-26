using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Clipboard
        public ActionResult Clipboard()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}