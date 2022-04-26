using System.Collections.Generic;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Mvc;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Core20.Controllers
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