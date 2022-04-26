﻿using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        //
        // GET: /VirtualScrolling/

        public ActionResult VirtualScrolling()
        {
            return View(Sale.GetData(100000));
        }

    }
}