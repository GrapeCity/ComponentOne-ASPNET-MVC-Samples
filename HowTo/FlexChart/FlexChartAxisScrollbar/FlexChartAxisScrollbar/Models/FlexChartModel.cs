using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFlexChartAxisScrollbar.Models
{
    public class FlexChartModel
    {
        public IDictionary<string, object[]> Settings { get; set; }
        public IEnumerable<CountryData> CountrySalesData { get; set; }

        public FlexChartModel()
        {

        }
    }
}