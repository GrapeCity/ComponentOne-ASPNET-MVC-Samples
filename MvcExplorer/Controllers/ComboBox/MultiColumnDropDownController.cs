using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class ComboBoxController
    {
        public ActionResult MultiColumnDropDown()
        {
            return View(Models.Cities.GetCities());
        }
    }
}
