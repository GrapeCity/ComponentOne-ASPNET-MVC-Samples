using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult RemoteBind()
        {
            return View();
        }

        public ActionResult RemoteBind_Read(CollectionViewRequest<CustomerOrder> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, CustomerOrder.GetCountryGroupOrderData()));
        }
    }
}
