﻿using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: FlexGridFocus
        public ActionResult FlexGridFocus()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}