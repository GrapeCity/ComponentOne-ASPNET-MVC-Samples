using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController 
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
                DefaultValues = new Dictionary<string, object> { { "AxisX.LabelAngle", 0 } }
            };

            return View(model);
        }

        private static IDictionary<string, object[]> CreateAxesSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                { "AxisX.LabelAngle", new object[] {-90, -45, 0, 45, 90} }
            };

            return settings;
        }
    }
}
