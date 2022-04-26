using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Images
        public ActionResult Images()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}