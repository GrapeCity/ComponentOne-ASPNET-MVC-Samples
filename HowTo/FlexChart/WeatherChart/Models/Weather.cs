using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherChart.Models
{
    public class Weather
    {
        private string[] _infos;
        private int _infosLength = 0;

        public Weather() { }

        public Weather(string info)
        {
            if (!string.IsNullOrEmpty(info))
            {
                _infos = info.Split(',');
                _infosLength = _infos.Count();
                InitialData();
            }
        }

        private void InitialData()
        {
            Date = _infosLength > 0 ? DateTime.Parse(_infos[0]) : DateTime.Today;
            MaxTemp = GetIntValue(1);
            MeanTemp = GetIntValue(2);
            MinTemp = GetIntValue(3);
            DewPoint = GetIntValue(4);
            MeanDewPoint = GetIntValue(5);
            MinDewpoint = GetIntValue(6);
            MaxHumidity = GetIntValue(7);
            MeanHumidity = GetIntValue(8);
            MinHumidity = GetIntValue(9);
            MaxPressure = GetIntValue(10);
            MeanPressure = GetIntValue(11);
            MinPressure = GetIntValue(12);
            MaxVisibility = GetIntValue(13);
            MeanVisibility = GetIntValue(14);
            MinVisibility = GetIntValue(15);
            MaxWindSpeed = GetIntValue(16);
            MeanWindSpeed = GetIntValue(17);
            MaxGustSpeed = GetIntValue(18);
            Precipitation = GetDoubleValue(19);
            CloudCover = GetIntValue(20);
            Events = GetStringValue(21);
            WindDirDegrees = GetIntValue(22);
        }

        public DateTime Date { get; set; }
        public int? MaxTemp { get; set; }
        public int? MeanTemp { get; set; }
        public int? MinTemp { get; set; }
        public int? DewPoint { get; set; }
        public int? MeanDewPoint { get; set; }
        public int? MinDewpoint { get; set; }
        public int? MaxHumidity { get; set; }
        public int? MeanHumidity { get; set; }
        public int? MinHumidity { get; set; }
        public int? MaxPressure { get; set; }
        public int? MeanPressure { get; set; }
        public int? MinPressure { get; set; }
        public int? MaxVisibility { get; set; }
        public int? MeanVisibility { get; set; }
        public int? MinVisibility { get; set; }
        public int? MaxWindSpeed { get; set; }
        public int? MeanWindSpeed { get; set; }
        public int? MaxGustSpeed { get; set; }
        public double? Precipitation { get; set; }
        public int? CloudCover { get; set; }
        public string Events { get; set; }
        public int? WindDirDegrees { get; set; }

        private int? GetIntValue(int index)
        {
            if (index < _infosLength)
            {
                int result;
                if (int.TryParse(_infos[index], out result))
                {
                    return result;
                }
            }
            return null;
        }

        private double? GetDoubleValue(int index)
        {
            if (index < _infosLength)
            {
                double result;
                if (double.TryParse(_infos[index], out result))
                {
                    return result;
                }
            }
            return null;
        }

        private string GetStringValue(int index)
        {
            if (index < _infosLength)
            {
                return _infos[index];
            }
            return string.Empty;
        }
    }
}