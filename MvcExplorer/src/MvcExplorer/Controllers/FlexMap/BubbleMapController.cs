using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexMapController : Controller
    {
        public ActionResult BubbleMap()
        {
            return View();
        }
    }
}
