using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: DefferedResizing
        public ActionResult DefferedResizing()
        {
            return View(Models.FlexGridData.GetSales(100));
        }
    }
}