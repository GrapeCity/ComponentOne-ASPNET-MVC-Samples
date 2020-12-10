using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    partial class TreeViewController : Controller
    {
        // GET: RemoteLoading
        public ActionResult RemoteLoading()
        {
            return View();
        }

        public ActionResult RemoteLoading_LoadAction()
        {
            return this.C1Json(Property.GetData(Url), false);
        }
    }
}