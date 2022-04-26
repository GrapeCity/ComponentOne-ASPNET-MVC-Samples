using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcExplorer.Models;

using Newtonsoft.Json;

namespace MvcExplorer.Controllers
{
    public class EnhancementsController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }
    }   
}
