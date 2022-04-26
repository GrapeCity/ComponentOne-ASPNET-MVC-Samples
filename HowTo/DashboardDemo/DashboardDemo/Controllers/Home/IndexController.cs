using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace DashboardDemo.Controllers
{
    public partial class HomeController : Controller
    {
        private static DataService _dataService;
        private static string _dataXmlFolderPath;
        public DataService DataService
        {
            get
            {
                return _dataService ?? (_dataService = DataService.GetService(DataXmlFolderPath));
            }
        }
        public string DataXmlFolderPath
        {
            get
            {
                return _dataXmlFolderPath ?? (_dataXmlFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/Content"));
            }
        }

        private void UpdateDateRange()
        {
            var range = new DateRange();
            var hasSet = false;
            var startDate = HttpContext.Request["startDate"];
            if(startDate != null)
            {
                range.Start = (DateTime.ParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture));
                hasSet = true;
            }
            var start = HttpContext.Request["start"];
            if (start != null)
            {
                range.NStart = Double.Parse(start);
                hasSet = true;
            }

            var endDate = HttpContext.Request["endDate"];
            if(endDate != null)
            {
                range.End = (DateTime.ParseExact(endDate, "M/d/yyyy", CultureInfo.InvariantCulture));
                hasSet = true;
            }

            var end = HttpContext.Request["end"];
            if(end != null)
            {
                range.NEnd = Double.Parse(end);
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
            ViewBag.RangeSelectorModel = new RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View();
        }

        [HttpPost]
        public ActionResult GetTopData()
        {
            UpdateDateRange();

            var respone = new ArrayList();
            var topProducts = DataService.GetTopSaleProductList(3);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleItem>(), topProducts));


            List<SaleItem> topCustomers = DataService.GetTopSaleCustomerList(7);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleItem>(), topCustomers));

            List<SaleGoalItem> goalItems = DataService.CategorySalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleGoalItem>(), goalItems));

            List<SaleGoalItem> regionalGoalItems = DataService.RegionSalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleGoalItem>(), regionalGoalItems));

            return this.C1Json(respone);
        }
    }
}
