using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Themes
        public ActionResult Themes()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}