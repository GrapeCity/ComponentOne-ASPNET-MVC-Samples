using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExplorer.Models
{
    public class WaterfallData
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public WaterfallData(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public static List<WaterfallData> GetData()
        {
            var names = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var data = new List<WaterfallData>();
            for (int i = 0, len = names.Length; i < len; i++)
            {
                data.Add(new WaterfallData(names[i], (((i % 3) + 3) * 1000)));
            };
            return data;
        }
    }
}