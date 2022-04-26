using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVChaining
        public ActionResult CVChaining()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}