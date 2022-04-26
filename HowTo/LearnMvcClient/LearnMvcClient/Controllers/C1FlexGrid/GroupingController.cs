using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Grouping
        public ActionResult Grouping()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}