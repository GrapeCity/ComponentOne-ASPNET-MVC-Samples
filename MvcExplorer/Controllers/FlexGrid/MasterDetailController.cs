using System.Linq;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult MasterDetail()
        {
            return View(db.Customers.Take(10).ToList());
        }

        public ActionResult DetailData([C1JsonRequest] CollectionViewRequest<Order> requestData)
        {
            string customerID = requestData.ExtraRequestData["CustomerID"].ToString();
            return this.C1Json(CollectionViewHelper.Read(requestData, db.Orders.Where(s => s.CustomerID == customerID).ToList()));
        }
    }
}
