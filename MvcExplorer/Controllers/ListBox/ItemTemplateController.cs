using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        public ActionResult ItemTemplate()
        {
            var list = MvcExplorer.Models.CustomerOrder.GetOrderData(100).ToList();
            return View(list);
        }
    }
}
