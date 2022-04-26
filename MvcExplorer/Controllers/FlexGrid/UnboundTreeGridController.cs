using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        // GET: UnboundTreeGrid
        public ActionResult UnboundTreeGrid()
        {
            var list = MvcExplorer.Models.Folder.Create(Server.MapPath("~")).Children;
            return View(list);
        }
    }
}