using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public IActionResult CustomColumnHeaders()
        {
            return View();
        }

        public ActionResult CustomColumnHeaders_Bind([C1JsonRequest] CollectionViewRequest<DataRepresentation> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, DataRepresentation.GetData(100)));
        }
    }
}