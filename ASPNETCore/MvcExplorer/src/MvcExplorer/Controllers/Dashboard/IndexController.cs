using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System;
using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace MvcExplorer.Controllers
{
    public partial class DashboardController : Controller
    {
        private static Models.DashboardModels.DataService _dataService;
        private string _dataXmlFolderPath;
        public Models.DashboardModels.DataService DataService
        {
            get
            {
                return _dataService ?? (_dataService = Models.DashboardModels.DataService.GetService(DataXmlFolderPath));
            }
        }
        public string DataXmlFolderPath
        {
            get
            {
                return _dataXmlFolderPath ?? (_dataXmlFolderPath = Startup.Environment.WebRootPath);
            }
        }

        private void UpdateDateRange()
        {
            var range = new Models.DashboardModels.DateRange();
            var hasSet = false;
            var requestData = HttpContext.Request.Query;
            if (requestData.ContainsKey("startDate"))
            {
                range.Start = (DateTime.ParseExact(requestData["startDate"], "M/d/yyyy", CultureInfo.InvariantCulture));
                hasSet = true;
            }
            if (requestData.ContainsKey("start"))
            {
                range.NStart = Double.Parse(requestData["start"]);
                hasSet = true;
            }
            if (requestData.ContainsKey("endDate"))
            {
                range.End = (DateTime.ParseExact(requestData["endDate"], "M/d/yyyy", CultureInfo.InvariantCulture));
                hasSet = true;
            }
            if (requestData.ContainsKey("end"))
            {
                range.NEnd = Double.Parse(requestData["end"]);
                hasSet = true;
            }

            if (hasSet)
            {
                DataService.DateRange = range;
            }
        }

        public ActionResult Index()
        {
            ViewBag.BudgetItemList = DataService.BudgetItemList;
            ViewBag.Top3SellingProducts = DataService.GetTopSaleProductList(3);
            ViewBag.Top7SaleCustomers = DataService.GetTopSaleCustomerList(7);
            ViewBag.CategorySalesVsGoal = DataService.CategorySalesVsGoal;
            ViewBag.RegionSalesVsGoal = DataService.RegionSalesVsGoal;
            ViewBag.RangeSelectorModel = new Models.DashboardModels.RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View();
        }

        public ActionResult GetTopData()
        {
            UpdateDateRange();
            var respone = new ArrayList();
            var topProducts = DataService.GetTopSaleProductList(3);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.SaleItem>(), topProducts));
            var topCustomers = DataService.GetTopSaleCustomerList(7);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.SaleItem>(), topCustomers));
            var goalItems = DataService.CategorySalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.SaleGoalItem>(), goalItems));
            var regionalGoalItems = DataService.RegionSalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.SaleGoalItem>(), regionalGoalItems));
            return this.C1Json(respone);
        }
    }
}
