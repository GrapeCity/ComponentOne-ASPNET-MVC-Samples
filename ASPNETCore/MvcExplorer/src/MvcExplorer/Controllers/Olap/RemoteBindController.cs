using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MvcExplorer.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    partial class OlapController : Controller
    {
        private static IEnumerable<ProductData> RemoteData = ProductData.GetData(1000).ToList();
        // GET: PivotGrid
        public ActionResult RemoteBind()
        {
            ViewBag.DemoSettings = true;
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }

        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<ProductData> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, RemoteData));
        }
    }
}