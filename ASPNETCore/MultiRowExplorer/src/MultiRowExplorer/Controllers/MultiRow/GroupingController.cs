using MultiRowExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        //
        // GET: /Grouping/

        public ActionResult Grouping()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    {"GroupBy", new object[]{"Customer State", "Customer City", "Customer State and Customer City", "Ordered Date", "Shipped Date", "Amount","None"}},
                    {"ShowGroups", new object[] {true, false } }
                }
            };

            return View();
        }

        public ActionResult Grouping_Bind([C1JsonRequest] CollectionViewRequest<Orders.Order> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Orders.GetOrders()));
        }
    }
}