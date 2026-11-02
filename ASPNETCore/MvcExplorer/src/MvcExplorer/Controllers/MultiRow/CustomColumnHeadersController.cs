using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public IActionResult CustomColumnHeaders()
        {
            return View();
        }

        public ActionResult CustomColumnHeaders_Bind([C1JsonRequest] CollectionViewRequest<DataRepresentationForMultiRow> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, DataRepresentationForMultiRow.GetData(100)));
        }
    }
}