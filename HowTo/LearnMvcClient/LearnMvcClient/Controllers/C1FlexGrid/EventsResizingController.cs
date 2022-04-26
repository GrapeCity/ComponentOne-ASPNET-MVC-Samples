using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: EventsResizing
        public ActionResult EventsResizing()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries7));
        }
    }
}