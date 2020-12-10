using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }

        public ActionResult RemoteBind()
        {
            return View();
        }
    }
}
