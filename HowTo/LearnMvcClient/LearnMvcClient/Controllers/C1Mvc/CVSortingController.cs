using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVSorting
        public ActionResult CVSorting()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}