using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: FlexGridFilter
        public ActionResult FlexGridFilter()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}