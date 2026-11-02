using System.Collections.Generic;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult GroupHeaders()
        {
            ViewBag.DemoSettingsModel = new ClientSettingsModel
            {
                Settings = new Dictionary<string, object[]>
                {
                    { "GroupBy", new object[] { "Country", "Color", "Country and Color", "None" } },
                    { "ShowGroups", new object[] {true, false } },
                    { "MultiRowGroupHeaders", new object[] {true, false } }
                }
            };

            return View();
        }

        public ActionResult GroupHeaders_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(100)));
        }
    }
}