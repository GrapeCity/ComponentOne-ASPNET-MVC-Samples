using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class FlexChartModel
    {
        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }

        public IEnumerable<CountryData> CountrySalesData { get; set; }

        public FlexChartModel()
        {

        }
    }
}
