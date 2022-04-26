using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Editing
        public ActionResult Editing()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}