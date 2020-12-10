using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult ColumnLayout()
        {
            var model = Sale.GetData(500);
            return View(model);
        }
    }
}
