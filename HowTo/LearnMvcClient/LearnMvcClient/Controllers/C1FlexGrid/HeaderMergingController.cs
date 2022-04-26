using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: HeaderMerging
        public ActionResult HeaderMerging()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries4, 20));
        }
    }
}