using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController : Controller
    {
        public ActionResult ComplexType()
        {
            var list = GetSystemColors();
            return View(list);
        }

        private static NamedColor[] GetSystemColors()
        {
            return Enum.GetValues(typeof(ConsoleColor))
                .Cast<ConsoleColor>()
                .Select(c => new NamedColor
                {
                    Name = c.ToString(),
                    Value = (int)c
                })
                .ToArray();
        }
    }
}
