using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace FinancialChartExplorer.Models
{
    public static class FbData
    {
        private static List<FinanceData> _jsonData;
        private static List<FinanceData> _omxData;
        private static List<FinanceData> _rsData;
        public static List<FinanceData> GetDataFromJson()
        {
            if (_jsonData != null)
            {
                return _jsonData;
            }

            string jsonText = new StreamReader(GetJsonStream("fb.json")).ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            var jDataset = (JObject)jo.GetValue("dataset");
            var jColumns = (JArray)jDataset.GetValue("column_names");
            var columnNames = new List<string>();
            foreach (var column in jColumns)
            {
                columnNames.Add(column.ToString());
            }
            var dateIndex = columnNames.IndexOf("Date"); //0
            var openIndex = columnNames.IndexOf("Open"); //1
            var highIndex = columnNames.IndexOf("High"); //2
            var lowIndex = columnNames.IndexOf("Low"); //3
            var closeIndex = columnNames.IndexOf("Close"); //4
            var volumeIndex = columnNames.IndexOf("Volume"); //5

            var jData = (JArray)jDataset.GetValue("data");
            List<FinanceData> list = new List<FinanceData>();
            foreach (JArray jItem in jData)
            {
                string date = jItem[dateIndex].ToString();
                double open = Convert.ToDouble(jItem[openIndex].ToString());
                double high = Convert.ToDouble(jItem[highIndex].ToString());
                double low = Convert.ToDouble(jItem[lowIndex].ToString());
                double close = Convert.ToDouble(jItem[closeIndex].ToString());
                double volume = Convert.ToDouble(jItem[volumeIndex].ToString());
                list.Add(new FinanceData { X = date, High = high, Low = low, Open = open, Close = close, Volume = volume });
            }
            _jsonData = list;
            return list;
        }

        private static List<FinanceData> GetOmxDataFromJson()
        {
            if (_omxData != null)
            {
                return _omxData;
            }

            string jsonText = new StreamReader(GetJsonStream("NDX.json")).ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);
            var jDataset = (JObject)jo.GetValue("dataset");
            var jColumns = (JArray)jDataset.GetValue("column_names");
            var columnNames = new List<string>();
            foreach (var column in jColumns)
            {
                columnNames.Add(column.ToString());
            }
            var dateIndex = columnNames.IndexOf("Trade Date"); //0
            var valueIndex = columnNames.IndexOf("Index Value"); //1
            var highIndex = columnNames.IndexOf("High"); //2
            var lowIndex = columnNames.IndexOf("Low"); //3

            var jData = (JArray)jDataset.GetValue("data");
            List<FinanceData> list = new List<FinanceData>();
            foreach (JArray jItem in jData)
            {
                string date = jItem[dateIndex].ToString();
                double value = Convert.ToDouble(jItem[valueIndex].ToString());
                double high = Convert.ToDouble(jItem[highIndex].ToString());
                double low = Convert.ToDouble(jItem[lowIndex].ToString());
                list.Add(new FinanceData { X = date, High = high, Low = low, Close = value });
            }
            _omxData = list;
            return list;
        }

        public static List<FinanceData> GetReativeStrengthDataFromJson()
        {
            if (_rsData != null)
            {
                return _rsData;
            }

            var list = new List<FinanceData>();
            var factor = 10000d;

            var omxData = GetOmxDataFromJson();
            foreach (var item in GetDataFromJson())
            {
                var omxItem = omxData.FirstOrDefault(d => d.X == item.X);
                if (omxItem != null)
                {
                    var close = item.Close / omxItem.Close * factor;
                    var high = item.High / omxItem.Close * factor;
                    var low = item.Low / omxItem.Close * factor;
                    list.Add(new FinanceData { X = item.X, High = high, Low = low, Close = close });
                }
            }

            _rsData = list;
            return list;
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
