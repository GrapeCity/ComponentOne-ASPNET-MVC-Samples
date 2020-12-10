using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace FinancialChartExplorer.Models
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
                string date = obj["date"].ToString();
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
                 Localization.Home.EventAnnotations_Tooltip1,
                 Localization.Home.EventAnnotations_Tooltip2,
                 Localization.Home.EventAnnotations_Tooltip3
                 +Localization.Home.EventAnnotations_Tooltip4,
                 Localization.Home.EventAnnotations_Tooltip5+
                 Localization.Home.EventAnnotations_Tooltip6,
                 Localization.Home.EventAnnotations_Tooltip7+
                 Localization.Home.EventAnnotations_Tooltip8,
                 Localization.Home.EventAnnotations_Tooltip9,
                 Localization.Home.EventAnnotations_Tooltip10+
                 Localization.Home.EventAnnotations_Tooltip11,
                 Localization.Home.EventAnnotations_Tooltip12+
                 Localization.Home.EventAnnotations_Tooltip13,
                 Localization.Home.EventAnnotations_Tooltip14,
                 Localization.Home.EventAnnotations_Tooltip15,
                 Localization.Home.EventAnnotations_Tooltip16+
                 Localization.Home.EventAnnotations_Tooltip17,
                 Localization.Home.EventAnnotations_Tooltip18+
                 Localization.Home.EventAnnotations_Tooltip19,
                 Localization.Home.EventAnnotations_Tooltip20+
                 Localization.Home.EventAnnotations_Tooltip21
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
