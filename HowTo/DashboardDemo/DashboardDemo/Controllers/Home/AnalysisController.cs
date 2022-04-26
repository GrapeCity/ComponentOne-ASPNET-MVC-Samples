using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        public ActionResult Analysis()
        {
            ViewBag.RegionSales = DataService.GetCategoryRegionWiseSales();
            ViewBag.OpportunityItemList = DataService.OpportunityItemList;
            ViewBag.ProductWiseSaleCollection = DataService.ProductWiseSaleCollection;
            ViewBag.RangeSelectorModel = new RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View();
        }

        public ActionResult GetAnalysisData()
        {
            UpdateDateRange();

            var respone = new ArrayList();
            var categoryRegionSale = DataService.GetCategoryRegionWiseSales();
            respone.Add(CollectionViewHelper.Read<ProductsWiseSaleItem>(new CollectionViewRequest<ProductsWiseSaleItem>(), categoryRegionSale));

            var opportunities = DataService.OpportunityItemList;
            respone.Add(CollectionViewHelper.Read<Opportunity>(new CollectionViewRequest<Opportunity>(), opportunities));

            return this.C1Json(respone);
        }

        //private List<Opportunity> GetAvailableList()
        //{
        //    var min = 0d;
        //    foreach(var item in DataService.OpportunityItemList)
        //    {
        //        if(min > item.Sales)
        //        {
        //            min = item.Sales;
        //        }
        //    }

        //    return min > 0 ? DataService.OpportunityItemList : null;
        //}
    }
}
