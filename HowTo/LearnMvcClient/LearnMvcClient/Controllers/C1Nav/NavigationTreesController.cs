using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: NavigationTrees
        public ActionResult NavigationTrees()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}