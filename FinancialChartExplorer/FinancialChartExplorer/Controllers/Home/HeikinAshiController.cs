﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinancialChartExplorer.Models;

namespace FinancialChartExplorer.Controllers
{
    public partial class HomeController : Controller
    {
        public ActionResult HeikinAshi()
        {
            var model = BoxData.GetDataFromJson();
            ViewBag.DemoSettingsModel = new ClientSettingsModel();
            ViewBag.ChartType = C1.Web.Mvc.Finance.ChartType.HeikinAshi;
            return View(model);
        }
    }
}
