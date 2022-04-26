using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVFiltering
        public ActionResult CVFiltering()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}