using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        // GET: ChildItems
        public ActionResult ChildItems()
        {
            ChildItems model = new ChildItems();
            return View(model);
        }

        
    }
}