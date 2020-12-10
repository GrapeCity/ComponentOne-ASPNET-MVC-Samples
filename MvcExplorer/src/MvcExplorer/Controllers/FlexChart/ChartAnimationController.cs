using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
    {
        public ActionResult ChartAnimation()
        {
            return View(Fruit.GetFruitsSales());
        }
    }
}
