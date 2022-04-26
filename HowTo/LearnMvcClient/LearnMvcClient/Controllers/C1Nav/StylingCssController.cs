using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: StylingCss
        public ActionResult StylingCss()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}