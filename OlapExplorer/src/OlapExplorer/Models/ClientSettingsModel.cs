using System.Collections.Generic;

namespace OlapExplorer.Models
{
    public class ClientSettingsModel
    {
        public string ControlId { get; set; }

        public IDictionary<string, object[]> Settings { get; set; }

        public IDictionary<string, object> DefaultValues { get; set; }
    }
}
