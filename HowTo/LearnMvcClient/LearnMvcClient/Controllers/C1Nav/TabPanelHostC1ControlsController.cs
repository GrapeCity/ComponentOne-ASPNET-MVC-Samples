using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1NavController : Controller
    {
        // GET: Adding
        public ActionResult TabPanelHostC1Controls()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}