using FlexChartAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlexChartAnalytics.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            FlexChartModal model = new FlexChartModal();
            model.Settings = CreateIndexSettings();
            model.MathPoints10 = MathPoint.GetMathPointList(10);
            model.MathPoints40 = MathPoint.GetMathPointList(40);
            model.MonthSales = MonthSale.GetData();

            return View(model);
        }

        private IDictionary<string, object[]> CreateIndexSettings()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"TrendLineFitType", new object[]{"Linear", "Exponential", "Logarithmic", "Power", "Fourier", "Polynomial", "MinX", "MinY", "MaxX", "MaxY", "AverageX", "AverageY"}},
                {"MovingAverageType", new object[]{"Simple", "Weighted", "Exponential", "Triangular"}},
            };

            return settings;
        }
    }
}
