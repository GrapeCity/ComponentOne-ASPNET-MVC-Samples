using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CustomFilterTypes
        public ActionResult CustomFilterTypes()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}