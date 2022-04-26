using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: EventsSelection
        public ActionResult EventsSelection()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries4, 20));
        }
    }
}