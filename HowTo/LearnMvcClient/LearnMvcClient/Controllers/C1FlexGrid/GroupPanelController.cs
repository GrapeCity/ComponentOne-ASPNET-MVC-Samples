using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: GroupPanel
        public ActionResult GroupPanel()
        {
            return View(Models.FlexGridData.GetSales(200));
        }
    }
}