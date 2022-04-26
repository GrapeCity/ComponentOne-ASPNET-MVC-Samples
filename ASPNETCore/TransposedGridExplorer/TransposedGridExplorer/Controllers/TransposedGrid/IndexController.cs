using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TransposedGridExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
#if !NETCORE31 && !NET50
using Microsoft.AspNetCore.Http.Internal;
#endif
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;

namespace TransposedGridExplorer.Controllers
{
    public partial class TransposedGridController : Controller
    {
        public ActionResult Index()
        {
            return View(ProductSheet.GetData());
        }
    }
}

