using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController : Controller
    {
        public ActionResult ItemTemplate()
        {
            var list = GetSystemColors();
            return View(list);
        }
    }
}
