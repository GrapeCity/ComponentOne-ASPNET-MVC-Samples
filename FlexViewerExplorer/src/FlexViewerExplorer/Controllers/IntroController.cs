﻿using Microsoft.AspNetCore.Mvc;

namespace FlexViewerExplorer.Controllers
{
    public partial class FlexViewerController : Controller
    {
        // GET: Intro
        public ActionResult Intro()
        {
            return View();
        }
    }
}