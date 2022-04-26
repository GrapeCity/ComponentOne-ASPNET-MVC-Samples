using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Disabling
        public ActionResult Disabling()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}