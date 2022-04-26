using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using DashboardModel;
using System;
using System.Collections;
using System.Collections.Generic;
#if ASPNETCORE
using Microsoft.AspNetCore.Mvc;
#else
using System.Web.Mvc;
#endif

namespace DashboardDemo.Controllers
{
    public partial class HomeController
    {
        public ActionResult Tasks()
        {
            ViewBag.RangeSelectorModel = new RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View();
        }

        public ActionResult Group_Bind(CollectionViewRequest<CampainTaskItem> requestData)
        {
            List<CampainTaskItem> items = DataService.CampainTaskCollction;
            return this.C1Json(CollectionViewHelper.Read(requestData, items));
        }

        public ActionResult GetTasks()
        {
            UpdateDateRange();

#if ASPNETCORE
            string strType = this.HttpContext.Request.Query["taskType"];
#else
            string strType = this.HttpContext.Request["taskType"];
#endif
            var type = !string.IsNullOrEmpty(strType) ? Convert.ToInt32(strType) : 0;
            var respone = new ArrayList();
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<CampainTaskItem>(), DataService.GetTaskItem((CampainTaskType)type)));

            return this.C1Json(respone);
        }
    }
}
