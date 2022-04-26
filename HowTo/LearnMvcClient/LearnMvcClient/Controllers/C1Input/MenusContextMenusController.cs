using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1InputController : Controller
    {
        // GET: MenusContextMenus
        public ActionResult MenusContextMenus()
        {
            return View(Models.FlexChartData.GetSales1());
        }
    }
}