using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ListBoxController
    {
        public ActionResult MultiColumn()
        {
            return View(Models.Cities.GetCities());
        }

    }
}
