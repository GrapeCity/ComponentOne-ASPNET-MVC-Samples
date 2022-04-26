using System.Collections.Generic;

namespace TransposedMultiRowExplorer.Models
{
    public class ClientSettingsModel
    {
        public string ControlId { get { return "DemoControl"; } }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }
    }
}