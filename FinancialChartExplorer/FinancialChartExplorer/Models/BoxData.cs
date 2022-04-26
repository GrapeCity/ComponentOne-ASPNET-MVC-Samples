using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

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

            string path = HttpContext.Current.Server.MapPath("~/Content/box.json");
            string jsonText = new StreamReader(path, System.Text.Encoding.Default).ReadToEnd();
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
                Resources.Home.EventAnnotations_Tooltip1,
                Resources.Home.EventAnnotations_Tooltip2,
                Resources.Home.EventAnnotations_Tooltip3
                +Resources.Home.EventAnnotations_Tooltip4,
                Resources.Home.EventAnnotations_Tooltip5+
                Resources.Home.EventAnnotations_Tooltip6,
                Resources.Home.EventAnnotations_Tooltip7+
                Resources.Home.EventAnnotations_Tooltip8,
                Resources.Home.EventAnnotations_Tooltip9,
                Resources.Home.EventAnnotations_Tooltip10+
                Resources.Home.EventAnnotations_Tooltip11,
                Resources.Home.EventAnnotations_Tooltip12+
                Resources.Home.EventAnnotations_Tooltip13,
                Resources.Home.EventAnnotations_Tooltip14,
                Resources.Home.EventAnnotations_Tooltip15,
                Resources.Home.EventAnnotations_Tooltip16+
                Resources.Home.EventAnnotations_Tooltip17,
                Resources.Home.EventAnnotations_Tooltip18+
                Resources.Home.EventAnnotations_Tooltip19,
                Resources.Home.EventAnnotations_Tooltip20+
                Resources.Home.EventAnnotations_Tooltip21
            };

            return _annotationToolTips;
        }
    }
}