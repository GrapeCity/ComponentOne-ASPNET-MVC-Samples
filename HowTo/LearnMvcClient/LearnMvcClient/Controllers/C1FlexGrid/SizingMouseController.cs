using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: SizingMouse
        public ActionResult SizingMouse()
        {
            return View(Models.FlexGridData.GetSales(50));
        }
    }
}