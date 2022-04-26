using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController : Controller
    {
        public ActionResult ComplexType()
        {
            var list = GetSystemColors();
            return View(list);
        }

        private static NamedColor[] GetSystemColors()
        {
            return Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Select(c => new NamedColor
                {
                    Name = c.ToString(),
                    Value = "#" + Color.FromKnownColor(c).ToArgb().ToString("X8").Substring(2)
                })
                .ToArray();
        }
    }
}
