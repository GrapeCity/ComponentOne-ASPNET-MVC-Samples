using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class UndoController : Controller
    {
        public ActionResult Index(FormCollection data)
        {
            return View(Sale.GetData(50));
        }
    }
}
