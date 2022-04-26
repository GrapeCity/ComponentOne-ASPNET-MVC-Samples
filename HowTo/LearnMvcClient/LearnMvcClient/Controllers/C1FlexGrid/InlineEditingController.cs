using System.Web.Mvc;

namespace LearnMvcClient.Controllers
{
    public partial class C1FlexGridController : Controller
    {
        // GET: InlineEditing
        public ActionResult InlineEditing()
        {
            return View(Models.FlexGridData.GetSales(Models.FlexGridData.Countries7));
        }
    }
}