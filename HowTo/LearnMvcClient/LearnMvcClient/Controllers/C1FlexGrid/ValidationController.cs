using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Validation
        public ActionResult Validation()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries7));
        }
    }
}