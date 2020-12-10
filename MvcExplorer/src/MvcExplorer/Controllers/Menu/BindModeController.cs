using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MenuController : Controller
    {
        public ActionResult BindMode()
        {
            var list = new List<Tuple<string, string>>();
            list.Add(Tuple.Create("Color: red", "red"));
            list.Add(Tuple.Create("Color: green", "green"));
            list.Add(Tuple.Create("Color: blue", "blue"));
            return View(list);
        }
    }
}
