using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class MultiSelectController
    {
        public ActionResult MultiColumnDropDown()
        {
            return View(Models.Cities.GetCities());
        }
    }
}
