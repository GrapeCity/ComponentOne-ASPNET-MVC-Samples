using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FinancialChartController : Controller
    {
        public ActionResult MovingAverages()
        {
            var model = BoxData.GetDataFromJson();
            return View(model);
        }
    }
}
