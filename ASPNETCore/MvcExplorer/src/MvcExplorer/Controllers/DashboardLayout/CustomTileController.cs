using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace MvcExplorer.Controllers
{
    partial class DashboardLayoutController
    {
        // GET: /<controller>/
        public IActionResult CustomTile()
        {
            return View(new DashboardData());
        }
    }
}
