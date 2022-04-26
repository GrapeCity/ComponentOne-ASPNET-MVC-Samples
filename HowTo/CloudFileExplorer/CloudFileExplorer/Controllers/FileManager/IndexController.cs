using System.Web.Mvc;

namespace CloudFileExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}