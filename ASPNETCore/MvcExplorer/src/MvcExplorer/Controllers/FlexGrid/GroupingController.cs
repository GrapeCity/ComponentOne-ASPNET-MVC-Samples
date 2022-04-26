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
        // GET: /Grouping/

        public ActionResult Grouping()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupBy", new object[]{"ShipCountry", "ShipCity", "ShipCountry and ShipCity", "Freight", "ShippedDate","None"}}
                }
            };            
            return View();
        }

        public ActionResult Grouping_Bind([C1JsonRequest] CollectionViewRequest<Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, _db.Orders.ToList()));
        }

    }
}
