using System.Web.Mvc;


namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult TreeGrid()
        {
            var list = MvcExplorer.Models.Folder.Create(Server.MapPath("~")).Children;
            return View(list);
        }
    }
}
