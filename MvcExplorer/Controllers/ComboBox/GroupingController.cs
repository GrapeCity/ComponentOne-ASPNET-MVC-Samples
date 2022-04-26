using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController : Controller
    {
        // GET: Grouping
        public ActionResult Grouping()
        {
            var nwind = new C1NWindEntities();
            
            return View(nwind.Suppliers.Take(20));
        }
    }
}