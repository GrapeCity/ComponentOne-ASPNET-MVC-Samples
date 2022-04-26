using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System;
using System.Web.Mvc;

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        public ActionResult Products()
        {
            ViewBag.BasePath = Request.ApplicationPath;
            return View();
        }

        public ActionResult GetProducts()
        {
            var strType = HttpContext.Request["productType"];
            var type = !string.IsNullOrEmpty(strType) ? Convert.ToInt32(strType) : 0;

            return this.C1Json(CollectionViewHelper.Read(new CollectionViewRequest<ProductItem>(), DataService.GetProductItemList((CategoryType)type)));
        }
    }
}
