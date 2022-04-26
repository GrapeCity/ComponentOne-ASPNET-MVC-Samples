using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult ColumnPicker(FormCollection collection)
        {
            var model = Sale.GetData(50);
            return View(model);
        }
    }
}