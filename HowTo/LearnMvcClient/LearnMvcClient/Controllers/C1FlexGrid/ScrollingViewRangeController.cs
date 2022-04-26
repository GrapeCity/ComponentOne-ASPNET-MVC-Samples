using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: ScrollingViewRange
        public ActionResult ScrollingViewRange()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}