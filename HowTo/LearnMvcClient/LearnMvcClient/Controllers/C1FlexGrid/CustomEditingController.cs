using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: CustomEditing
        public ActionResult CustomEditing()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries7));
        }
    }
}