using C1.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MvcExplorer.Models;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult FrozenCells()
        {
            ViewBag.DemoSettings = true;
            
            return View();
        }

        public ActionResult FrozenCells_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            return this.C1Json(CollectionViewHelper.Read(requestData, Sale.GetData(500)));
        }
        
    }
}
