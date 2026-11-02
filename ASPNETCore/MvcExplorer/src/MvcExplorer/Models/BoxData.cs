using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace MvcExplorer.Models
{
    public static class BoxData
    {
        private static List<FinanceData> _jsonData;
        public static List<FinanceData> GetDataFromJson()
        {
            if (_jsonData != null)
            {
                return _jsonData;
            }
            string jsonText = new StreamReader(GetJsonStream("box.json")).ReadToEnd();
            JArray ja = (JArray)JsonConvert.DeserializeObject(jsonText);
            List<FinanceData> list = new List<FinanceData>();
            foreach (var obj in ja)
            {
                DateTime date = DateTime.Parse(obj["date"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                double high = Convert.ToDouble(obj["high"].ToString());
                double low = Convert.ToDouble(obj["low"].ToString());
                double open = Convert.ToDouble(obj["open"].ToString());
                double close = Convert.ToDouble(obj["close"].ToString());
                double volume = Convert.ToDouble(obj["volume"].ToString());
                list.Add(new FinanceData { X = date, High = high, Low = low, Open = open, Close = close, Volume = volume });
            }
            _jsonData = list;
            return list;
        }

        private static List<string> _annotationToolTips;
        public static List<string> GetAnnotationTooltips()
        {
            if (_annotationToolTips != null)
            {
                return _annotationToolTips;
            }
            _annotationToolTips = new List<string>{
                 Localization.FinancialChartRes.EventAnnotations_Tooltip1,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip2,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip3
                 +Localization.FinancialChartRes.EventAnnotations_Tooltip4,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip5+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip6,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip7+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip8,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip9,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip10+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip11,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip12+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip13,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip14,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip15,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip16+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip17,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip18+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip19,
                 Localization.FinancialChartRes.EventAnnotations_Tooltip20+
                 Localization.FinancialChartRes.EventAnnotations_Tooltip21
            };
            return _annotationToolTips;
        }

        public static Stream GetJsonStream(string name)
        {
            var assembly = typeof(ControlPages).GetTypeInfo().Assembly;
            var ress = assembly.GetManifestResourceNames();
            var res = assembly.GetManifestResourceNames().Where(resName => resName.Contains(name)).ToList();
            if (res.Count == 0)
            {
                throw new ArgumentOutOfRangeException("name");
            }
            return assembly.GetManifestResourceStream(res[0]);
        }
    }
}
