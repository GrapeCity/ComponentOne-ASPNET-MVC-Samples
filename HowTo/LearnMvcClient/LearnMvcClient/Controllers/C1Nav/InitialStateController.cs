using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: InitialState
        public ActionResult InitialState()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}