using System.Web.Mvc;
using MultiRowExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }

        public ActionResult RemoteBind()
        {
            return View();
        }
    }
}
