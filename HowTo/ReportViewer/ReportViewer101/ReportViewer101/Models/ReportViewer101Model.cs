using System.Collections.Generic;

namespace ReportViewer101.Models
{
    public class ReportViewer101Model
    {
        public string ServiceUrl { get; set; }
        public double ZoomFactor { get; set; }
        public List<CmbList> ReportNames { get; set; }
        public List<CmbList> Parameters { get; set; }
        public List<CmbList> MouseModes { get; set; }
        public ReportViewer101Model()
        {

        }
    }

    public class CmbList
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public CmbList(string value, string text)
        {
            Text = text;
            Value = value;
        }
    }
}