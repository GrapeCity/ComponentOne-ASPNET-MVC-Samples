using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}