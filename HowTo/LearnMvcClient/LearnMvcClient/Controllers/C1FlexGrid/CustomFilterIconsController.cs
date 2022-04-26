using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CustomFilterIcons
        public ActionResult CustomFilterIcons()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}