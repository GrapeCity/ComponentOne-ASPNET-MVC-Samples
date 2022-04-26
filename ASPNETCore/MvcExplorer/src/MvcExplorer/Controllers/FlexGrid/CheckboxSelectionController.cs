using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
    //
    // GET: /CheckboxSelection/

    public ActionResult CheckboxSelection()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"CheckboxColumn", new object[]{"Header", "OrderID", "ShipCountry", "ShipCity"}},
                    {"ShowCheckAll", new object[]{"True", "False"}}
                }
            };            
            return View();
        }

        public ActionResult CheckboxSelection_Bind([C1JsonRequest] CollectionViewRequest<Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Orders.Take(100).ToList()));
        }

    }
}
