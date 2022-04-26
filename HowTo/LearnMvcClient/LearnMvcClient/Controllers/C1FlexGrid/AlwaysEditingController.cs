using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: AlwaysEditing
        public ActionResult AlwaysEditing()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries7));
        }
    }
}