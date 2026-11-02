using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult Styling()
        {
            return View();
        }

        public ActionResult Styling_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }
    }
}