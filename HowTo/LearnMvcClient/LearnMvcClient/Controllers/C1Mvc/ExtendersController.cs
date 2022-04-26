using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Extenders
        public ActionResult Extenders()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}