using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: GridPanels
        public ActionResult GridPanels()
        {
            return View(Models.FlexGridData.GetSales());
        }
    }
}