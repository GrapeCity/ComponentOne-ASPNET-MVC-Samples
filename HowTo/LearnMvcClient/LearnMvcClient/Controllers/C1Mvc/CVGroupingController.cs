using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVGrouping
        public ActionResult CVGrouping()
        {
            return View(Models.FlexGridData.GetSales(500));
        }
    }
}