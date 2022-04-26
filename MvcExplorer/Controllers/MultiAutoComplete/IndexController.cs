using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MultiAutoCompleteController : Controller
    {
        public ActionResult Index()
        {
            return View(Models.Countries.GetCountries());
        }
    }
}
