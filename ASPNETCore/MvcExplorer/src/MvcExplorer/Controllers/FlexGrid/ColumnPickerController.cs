using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult ColumnPicker()
        {
            var model = Sale.GetData(50);
            return View(model);
        }
    }
}
