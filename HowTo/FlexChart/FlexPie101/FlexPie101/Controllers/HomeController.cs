using Microsoft.AspNetCore.Mvc;
using FlexPie101.Models;
using System.Collections.Generic;
using C1.Web.Mvc.Chart;

namespace FlexPie101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FlexPieModel ModelObj = new FlexPieModel();
            ModelObj.Settings = CreateSettings();
            ModelObj.CountryGroupOrderData = CustomerOrder.GetCountryGroupOrderData();
            return View(ModelObj);
        }

        private static IDictionary<string, object[]> CreateSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"Palette", new object[]{"standard", "cocoa", "coral", "dark", "highcontrast", "light", "midnight", "minimal", "modern", "organic", "slate"}},
                {"LegendPosition", new object[]{Position.Top.ToString(), Position.Bottom.ToString(), Position.Left.ToString(), Position.None.ToString(), Position.Right.ToString()}},
                {"SelectedItemPosition", new object[]{Position.Top.ToString(), Position.Bottom.ToString(), Position.Left.ToString(), Position.None.ToString(), Position.Right.ToString()}},
            };
            return settings;
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
