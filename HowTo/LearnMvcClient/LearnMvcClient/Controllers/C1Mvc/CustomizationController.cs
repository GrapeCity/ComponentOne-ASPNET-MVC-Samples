using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: Customization
        public ActionResult Customization()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}