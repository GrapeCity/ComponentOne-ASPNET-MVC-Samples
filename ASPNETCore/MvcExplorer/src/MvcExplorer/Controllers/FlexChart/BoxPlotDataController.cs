using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexChartController : Controller
    {
        public ActionResult BoxPlotData()
        {
            var data = SalesAnalysis.GetData();

            // In a real application these statistics would already exist - computed by a
            // database query or a back-end service. The sample works them out once here so
            // the chart never has to parse the raw arrays again while rendering.
            ViewBag.DownloadsStats = data.Select(d => GetBoxPlotData(d.Downloads)).ToArray();
            ViewBag.SalesStats = data.Select(d => GetBoxPlotData(d.Sales)).ToArray();

            // The Queries series is left without statistics on purpose, to show the
            // fallback: those points are still calculated from the bound raw array.
            return View(data);
        }

        private static BoxPlotData GetBoxPlotData(IList<int> values)
        {
            var sorted = values.OrderBy(v => v).Select(v => (double)v).ToList();
            var q1 = Quantile(sorted, 0.25);
            var q3 = Quantile(sorted, 0.75);
            var iqr = q3 - q1;
            var lower = q1 - 1.5 * iqr;
            var upper = q3 + 1.5 * iqr;
            var inRange = sorted.Where(v => v >= lower && v <= upper).ToList();


            var min = inRange.Count > 0 ? inRange.First() : sorted.First();
            var max = inRange.Count > 0 ? inRange.Last() : sorted.Last();

            return new BoxPlotData
            {
                Min = min,
                Max = max,
                Mean = sorted.Average(),
                FirstQuartile = q1,
                ThirdQuartile = q3,
                Median = Quantile(sorted, 0.5),
                OutlierPoints = sorted.Where(v => v < lower || v > upper).ToArray(),
                InnerPoints = sorted.Where(v => v > min && v < max).ToArray()
            };
        }

        private static double Quantile(IList<double> sorted, double q)
        {
            var pos = (sorted.Count - 1) * q;
            var lo = (int)Math.Floor(pos);
            var hi = (int)Math.Ceiling(pos);
            return lo == hi ? sorted[lo] : sorted[lo] + (sorted[hi] - sorted[lo]) * (pos - lo);
        }
    }
}
