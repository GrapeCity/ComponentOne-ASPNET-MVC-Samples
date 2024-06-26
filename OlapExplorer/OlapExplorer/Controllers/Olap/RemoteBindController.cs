﻿using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using OlapExplorer.Models;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace OlapExplorer.Controllers.Olap
{
    partial class OlapController : Controller
    {
        private static IEnumerable<ProductData> RemoteData = ProductData.GetData(1000).ToList();
        // GET: PivotGrid
        public ActionResult RemoteBind()
        {
            OlapModel.ControlId = "remotePanel";
            ViewBag.DemoSettingsModel = OlapModel;
            return View();
        }

        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<ProductData> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, RemoteData));
        }
    }
}