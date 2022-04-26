using FilterPanel.Models;
using System.Linq;
using System.Web.Mvc;

namespace FilterPanel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(Sale.GetData(100).ToList());
        }
    }
}