using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFlexChartAxisScrollbar.Models;

namespace MVCFlexChartAxisScrollbar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FlexChartModel ModelObj = new FlexChartModel();
            ModelObj.CountrySalesData = CountryData.GetCountryData();
            return View(ModelObj);
        }        
    }
}