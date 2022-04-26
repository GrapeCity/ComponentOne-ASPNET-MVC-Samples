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
        // GET: CustomColumnHeaders
        public ActionResult CustomColumnHeaders()
        {
            return View(DataRepresentation.GetData(100));
        }
    }
}