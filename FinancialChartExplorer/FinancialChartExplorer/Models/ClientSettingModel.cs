using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancialChartExplorer.Models
{
    public class ClientSettingsModel
    {
        public virtual string ControlId
        {
            get { return "DemoControl"; }
        }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }
    }
}