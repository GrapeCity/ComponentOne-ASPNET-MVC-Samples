using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult MultipleChart()
        {
            return View(ProductSales.GetData());
        }
    }
}
