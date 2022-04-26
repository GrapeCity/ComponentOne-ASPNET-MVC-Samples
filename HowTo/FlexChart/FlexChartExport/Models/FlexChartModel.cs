using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChartExport.Models
{
    //FlexGrid Model Class Declaration
    public class FlexChartModel
    {
        public IDictionary<string, object[]> Settings { get; set; }        
        public IEnumerable<CountryData> CountrySalesData { get; set; }

        //Class Constructor
        public FlexChartModel()
        {

        }
    }
}