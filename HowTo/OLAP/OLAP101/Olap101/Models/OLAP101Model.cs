using System.Collections.Generic;

namespace Olap101.Models
{
    public class OLAP101Model
    {
        public string ControlId { get; set; }
        public IDictionary<string, object[]> Settings { get; set; }
        public IDictionary<string, object> DefaultValues { get; set; }
        public IEnumerable<ProductData> Data { get; set; }

        public OLAP101Model()
        {
        }
    }
}