using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System;
#if ASPNETCORE
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
#endif

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        public ActionResult Products()
        {
#if ASPNETCORE
            ViewBag.BasePath = this.Request.PathBase;

#else
            ViewBag.BasePath = this.Request.ApplicationPath;
#endif
            return View();
        }

        public ActionResult GetProducts()
        {
#if ASPNETCORE
            string strType = this.HttpContext.Request.Query["productType"];
#else
            string strType = this.HttpContext.Request["productType"];
#endif
            int type = !string.IsNullOrEmpty(strType) ? Convert.ToInt32(strType) : 0;

            return this.C1Json(CollectionViewHelper.Read(new CollectionViewRequest<ProductItem>(), DataService.GetProductItemList((CategoryType)type)));
        }
    }
}
