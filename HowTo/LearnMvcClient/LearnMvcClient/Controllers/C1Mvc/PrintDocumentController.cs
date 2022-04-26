using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: PrintDocument
        public ActionResult PrintDocument()
        {
            return View(Models.FlexGridData.GetSales(100));
        }
    }
}