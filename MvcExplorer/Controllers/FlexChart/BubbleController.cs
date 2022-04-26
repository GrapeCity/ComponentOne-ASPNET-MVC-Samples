using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Bubble()
        {
            List<Dot> bubbles = new List<Dot>();
            var rand = new Random(0);
            for (int i = 0; i < 30; i++)
            {
                double x = i;
                double y = rand.Next(1, 10);
                double size = rand.Next(1, 10);
                bubbles.Add(new Dot { X = x, Y = y * 10, Size = size * 100 });
            }
            return View(bubbles);
        }
    }
}
