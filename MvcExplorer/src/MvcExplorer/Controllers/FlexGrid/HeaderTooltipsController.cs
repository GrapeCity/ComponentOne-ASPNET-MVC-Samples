using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MvcExplorer.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult HeaderTooltips(IFormCollection collection)
        {
            return View();
        }

        public ActionResult HeaderTooltips_Bind([C1JsonRequest] CollectionViewRequest<Sale> requestData)
        {
            var model = Sale.GetData(50);
            return this.C1Json(CollectionViewHelper.Read(requestData, model));
        }
    }
}
