using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Rtl
        public ActionResult Rtl()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}