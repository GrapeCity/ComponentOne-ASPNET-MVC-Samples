using System;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult RangeArea()
        {
            return View(Fruit.GetFruitsSales());
        }
    }
}
