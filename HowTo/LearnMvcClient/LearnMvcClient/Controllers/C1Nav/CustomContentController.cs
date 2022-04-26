using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: CustomContent
        public ActionResult CustomContent()
        {
            return View(Models.TreeViewData.GetData(Url));
        }
    }
}