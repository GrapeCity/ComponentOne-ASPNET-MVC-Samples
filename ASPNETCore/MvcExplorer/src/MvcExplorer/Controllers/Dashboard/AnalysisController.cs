using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class DashboardController
    {
        public ActionResult Analysis()
        {
            ViewBag.RegionSales = DataService.GetCategoryRegionWiseSales();
            ViewBag.OpportunityItemList = DataService.OpportunityItemList;
            ViewBag.RangeSelectorModel = new Models.DashboardModels.RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View(DataService.ProductWiseSaleCollection);
        }

        public ActionResult GetAnalysisData()
        {
            this.UpdateDateRange();

            ArrayList respone = new ArrayList();
            List<Models.DashboardModels.ProductsWiseSaleItem> categoryRegionSale = DataService.GetCategoryRegionWiseSales();
            respone.Add(CollectionViewHelper.Read<Models.DashboardModels.ProductsWiseSaleItem>(new CollectionViewRequest<Models.DashboardModels.ProductsWiseSaleItem>(), categoryRegionSale));

            var opportunities = DataService.OpportunityItemList;
            respone.Add(CollectionViewHelper.Read<Models.DashboardModels.Opportunity>(new CollectionViewRequest<Models.DashboardModels.Opportunity>(), opportunities));

            return this.C1Json(respone);
        }

        //private List<Opportunity> GetAvailableList()
        //{
        //    var min = 0d;
        //    foreach (var item in DataService.OpportunityItemList)
        //    {
        //        if (min < item.Sales)
        //        {
        //            min = item.Sales;
        //        }
        //    }

        //    return min > 0 ? DataService.OpportunityItemList : null;
        //}
    }
}
