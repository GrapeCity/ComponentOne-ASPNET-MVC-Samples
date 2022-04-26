using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using System.Data;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectController : Controller
    {
        private C1NWindEntities db = new C1NWindEntities();
        public ActionResult ComplexType()
        {
            return View(db);
        }
    }

}

