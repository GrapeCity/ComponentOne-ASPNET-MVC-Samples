using System.Collections.Generic;

namespace FlexPie101.Models
{
    public class FlexPieModel
    {
        public virtual string ControlId
        {
            get { return "DemoControl"; }
        }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }

        public IEnumerable<CustomerOrder> CountryGroupOrderData { get; set; }

    }
}