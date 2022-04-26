using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EditableAnnotationLayer.Models;

namespace EditableAnnotationLayer.Controllers
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