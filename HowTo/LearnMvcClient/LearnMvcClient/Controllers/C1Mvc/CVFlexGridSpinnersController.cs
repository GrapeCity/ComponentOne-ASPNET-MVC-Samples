using System.Web.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Threading;

namespace LearnMvcClient.Controllers
{
    public partial class C1MvcController : Controller
    {
        // GET: CVFlexGridSpinners
        public ActionResult CVFlexGridSpinners()
        {
            return View();
        }

        public ActionResult CVFlexGridSpinners_Read([C1JsonRequest] CollectionViewRequest<Models.FlexGridData.Sale> requestData)
        {
            Thread.Sleep(3000);
            return this.C1Json(CollectionViewHelper.Read(requestData, Models.FlexGridData.GetSales(200)));
        }
    }
}