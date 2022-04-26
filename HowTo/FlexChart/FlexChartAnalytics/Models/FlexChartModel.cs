using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChartAnalytics.Models
{
    public class FlexChartModal
    {
        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }

        public IEnumerable<MathPoint> MathPoints10 { get; set; }

        public IEnumerable<MathPoint> MathPoints40 { get; set; }

        public IEnumerable<MonthSale> MonthSales { get; set; }
    }
}