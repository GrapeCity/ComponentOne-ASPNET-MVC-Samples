using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: EventsMouse
        public ActionResult EventsMouse()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries4, 20));
        }
    }
}