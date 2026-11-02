using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;


namespace MvcExplorer.Controllers
{
    public partial class DashboardController
    {
        public ActionResult Tasks()
        {
            ViewBag.RangeSelectorModel = new Models.DashboardModels.RangeSelectorModel
            {
                DateSaleList = DataService.DateSaleList,
                Range = DataService.DateRange
            };
            return View();
        }

        public ActionResult Group_Bind(CollectionViewRequest<Models.DashboardModels.CampainTaskItem> requestData)
        {
            List<Models.DashboardModels.CampainTaskItem> items = DataService.CampainTaskCollction;
            return this.C1Json(CollectionViewHelper.Read(requestData, items));
        }

        public ActionResult GetTasks()
        {
            UpdateDateRange();

            string strType = this.HttpContext.Request.Query["taskType"];

            var type = !string.IsNullOrEmpty(strType) ? Convert.ToInt32(strType) : 0;
            var respone = new ArrayList();
            respone.Add(CollectionViewHelper.Read(new CollectionViewRequest<Models.DashboardModels.CampainTaskItem>(), DataService.GetTaskItem((Models.DashboardModels.CampainTaskType)type)));

            return this.C1Json(respone);
        }
    }
}
