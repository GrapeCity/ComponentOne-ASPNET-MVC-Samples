using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: NoScrollbars
        public ActionResult NoScrollbars()
        {
            return View(Models.FlexGridData.GetSales(50));
        }
    }
}