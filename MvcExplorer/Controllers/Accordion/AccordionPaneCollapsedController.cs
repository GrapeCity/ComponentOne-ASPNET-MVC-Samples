using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class AccordionController : Controller
    {
        // GET: AccordionPane
        public ActionResult AccordionPaneCollapsed()
        {
            return View();
        }
    }
}