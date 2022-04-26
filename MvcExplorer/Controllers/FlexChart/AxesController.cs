using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult Axes()
        {
            Dictionary<string, IEnumerable<CustomerOrder>> OrderDatas = new Dictionary<string, IEnumerable<CustomerOrder>>();
            OrderDatas.Add("2013", CustomerOrder.GetCountryCountOrderData(1, 5));
            OrderDatas.Add("2014", CustomerOrder.GetCountryCountOrderData(3, 10));
            OrderDatas.Add("CountryNames", CustomerOrder.GetCountryNameData());
            ViewBag.OrderDatas = OrderDatas;

            var model = new ClientSettingsModel
            {
                Settings = CreateAxesSettings(),
                DefaultValues = new Dictionary<string, object> { 
                    {"AxisX.LabelAngle", 0}
                }
            };

            return View(model);
        }

        private static IDictionary<string, object[]> CreateAxesSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"AxisX.LabelAngle", new object[]{-90, -45, 0, 45, 90}}
            };

            return settings;
        }
    }
}
