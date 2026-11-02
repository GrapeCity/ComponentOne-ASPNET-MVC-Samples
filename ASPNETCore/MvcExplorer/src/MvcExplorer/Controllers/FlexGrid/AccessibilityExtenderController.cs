using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult AccessibilityExtender()
        {
            return View(Sale.GetData(100));
        }
    }
}
