using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
