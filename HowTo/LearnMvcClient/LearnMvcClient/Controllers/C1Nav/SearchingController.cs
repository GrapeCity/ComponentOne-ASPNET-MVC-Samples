using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Searching
        public ActionResult Searching()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}