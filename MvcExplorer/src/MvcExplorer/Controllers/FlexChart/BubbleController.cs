using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
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
