using System.Collections.Generic;

namespace RazorPagesExplorer.Models
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