using System;
using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexPieController : Controller
    {
        public ActionResult RemoteBind_Read([C1JsonRequest] CollectionViewRequest<CustomerOrder> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, CustomerOrder.GetCountryGroupOrderData()));
        }

        public ActionResult RemoteBind()
        {
            return View();
        }
    }
}