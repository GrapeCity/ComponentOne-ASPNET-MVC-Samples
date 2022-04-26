using Newtonsoft.Json;
using System;

namespace MvcExplorer.Models
{
    public class FinanceData
    {
        [JsonProperty("date")]
        public DateTime X { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
    }
}