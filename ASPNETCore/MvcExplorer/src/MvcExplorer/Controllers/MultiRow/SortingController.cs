using MvcExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        public ActionResult Sorting()
        {
            return View();
        }

        public ActionResult Sorting_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
    }
}
