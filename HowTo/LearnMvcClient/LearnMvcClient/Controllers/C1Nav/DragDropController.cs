using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: DragDrop
        public ActionResult DragDrop()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}