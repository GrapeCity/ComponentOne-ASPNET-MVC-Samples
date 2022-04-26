using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System;
using System.Collections;
using System.Globalization;
#if ASPNETCORE
using Microsoft.AspNetCore.Mvc;
using System.IO;
#else
using System.Web.Mvc;
#endif

namespace DashboardDemo.Controllers
{
    public partial class HomeController : Controller
    {
        private static DataService _dataService;
        private string _dataXmlFolderPath;
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
                return _dataXmlFolderPath ?? (_dataXmlFolderPath = Path.Combine(Startup.Environment.WebRootPath, "Content"));
            }
        }

        private void UpdateDateRange()
        {
            var range = new DateRange();
            var hasSet = false;
#if ASPNETCORE
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
#else
            var requestData = HttpContext.Request;
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
#endif
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

        public ActionResult GetTopData()
        {
            UpdateDateRange();
            var respone = new ArrayList();
            var topProducts = DataService.GetTopSaleProductList(3);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleItem>(), topProducts));
            var topCustomers = DataService.GetTopSaleCustomerList(7);
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleItem>(), topCustomers));
            var goalItems = DataService.CategorySalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleGoalItem>(), goalItems));
            var regionalGoalItems = DataService.RegionSalesVsGoal;
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<SaleGoalItem>(), regionalGoalItems));
            return this.C1Json(respone);
        }
    }
}
