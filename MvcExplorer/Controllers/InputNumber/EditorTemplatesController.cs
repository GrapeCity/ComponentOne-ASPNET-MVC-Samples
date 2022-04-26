using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class InputNumberController : Controller
    {
        public ActionResult EditorTemplates()
        {
            var model = new Sale { ID = 101, Start = new DateTime(2014, 4,4,17,0,0), End = DateTime.Now, Amount = 1000};
            return View(model);
        }
    }
}
