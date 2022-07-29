using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class AccordionController : Controller
    {
        public ActionResult AccordionPaneCollapsed()
        {
            return View();
        }
    }
}
