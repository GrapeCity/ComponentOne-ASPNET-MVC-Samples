using System.Web.Mvc;

namespace CloudFileExplorer.Controllers
{
    public partial class FileManagerController : Controller
    {
        public ActionResult LocalStorage()
        {
            return View();
        }
    }
}