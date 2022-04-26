using System.Data.Entity.Validation;
using MultiRowLOB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace MultiRowLOB.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /OrderDetail/

        public ActionResult OrderDetail()
        {
            return View(DataService.GetOrderDetails(5));
        }
    }
}
