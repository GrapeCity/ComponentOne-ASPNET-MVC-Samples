using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MultiRowExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult CollapsedHeaders()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    { "CollapsedHeaders", new object[] {"False", "True", "null"} },
                    { "ShowHeaderCollapseButton", new object[] { false, true} }
                },
                DefaultValues=new Dictionary<string, object>()
                {
                    { "CollapsedHeaders", true },
                    { "ShowHeaderCollapseButton", true }
                }
            };
            return View();
        }

        public ActionResult CollapsedHeaders_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            var model = Orders.GetOrders();
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}