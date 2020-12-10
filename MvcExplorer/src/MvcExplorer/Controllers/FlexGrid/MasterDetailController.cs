using System.Linq;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        // GET: /<controller>/
        public ActionResult MasterDetail()
        {
            return View(_db.Customers.Take(10).ToList());
        }

        public ActionResult DetailData([C1JsonRequest] CollectionViewRequest<Order> requestData)
        {
            string customerID = requestData.ExtraRequestData["CustomerID"].ToString();
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Orders.Where(s => s.CustomerID == customerID).ToList()));
        }
    }
}
