using MvcExplorer.Models;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using C1.Web.Mvc.Olap;

namespace MvcExplorer.Controllers
{
    public partial class OlapController : Controller
    {
        // GET: PivotGrid
        public ActionResult Slicer()
        {
            return View(Data);
        }
    }
}