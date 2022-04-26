using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class CollectionViewController : Controller
    {
        public ActionResult CalculatedFields(FormCollection data)
        {
            var model = Sale.GetData(500);
            return View(model);
        }
    }
}