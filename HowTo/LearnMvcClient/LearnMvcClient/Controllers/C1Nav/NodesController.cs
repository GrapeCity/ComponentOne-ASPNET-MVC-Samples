using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Nodes
        public ActionResult Nodes()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}