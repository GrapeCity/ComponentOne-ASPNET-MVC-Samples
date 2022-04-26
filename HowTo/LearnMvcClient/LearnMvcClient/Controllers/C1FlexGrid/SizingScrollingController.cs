using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: SizingScrolling
        public ActionResult SizingScrolling()
        {
            return View(Models.FlexGridData.GetSales(50));
        }
    }
}