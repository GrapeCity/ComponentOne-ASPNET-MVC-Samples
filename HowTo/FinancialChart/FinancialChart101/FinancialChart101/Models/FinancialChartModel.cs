using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialChart101.Models
{
    public class FinancialChartModel
    {
        public string Header {get;set;}
        public IEnumerable<Data> SharesData { get; set; }
        public float PeriodMin { get; set; }
        public float PeriodMax { get; set; }
        public float PeriodValue { get; set; }
        public float PeriodStep { get; set; }
        public string Format { get; set; }
        public IDictionary<string, object[]> Settings { get; set; }

        public FinancialChartModel()
        {
            Header = "Facebook, Inc. (FB)";
            SharesData = Data.GetData();
            PeriodMin = 2;
            PeriodMax = 78;
            PeriodValue = 2;
            PeriodStep = 1;
            Format = "n0";
        }        
    }
}