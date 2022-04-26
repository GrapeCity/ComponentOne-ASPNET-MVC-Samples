using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: Filtering
        public ActionResult Filtering()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}