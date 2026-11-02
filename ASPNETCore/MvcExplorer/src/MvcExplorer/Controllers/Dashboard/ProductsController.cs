using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System;
using Microsoft.AspNetCore.Mvc;


namespace MvcExplorer.Controllers
{
    public partial class DashboardController
    {
        public ActionResult Products()
        {
            ViewBag.BasePath = this.Request.PathBase;
            return View();
        }

        public ActionResult GetProducts()
        {
            string strType = this.HttpContext.Request.Query["productType"];
            int type = !string.IsNullOrEmpty(strType) ? Convert.ToInt32(strType) : 0;

            return this.C1Json(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.ProductItem>(), DataService.GetProductItemList((Models.DashboardModels.CategoryType)type)));
        }
    }
}
