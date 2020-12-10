using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController
    {
        // GET: /<controller>/
        public ActionResult Scaling()
        {
            return View(Population.GetData());
        }
    }
}
