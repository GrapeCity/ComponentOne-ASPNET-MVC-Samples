using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /VirtualScrolling/

        public ActionResult VirtualScrolling()
        {
            return View(Sale.GetData(100000));
        }

    }
}
