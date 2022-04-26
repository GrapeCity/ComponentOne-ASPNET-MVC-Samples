using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Checkboxes
        public ActionResult Checkboxes()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}