using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class HierarchicalData
    {
        public int Year { get; set; }
        public string Quarter { get; set; }
        public string Month { get; set; }
        public int Value { get; set; }
        public HierarchicalData(int year, string quarter, string month, int value)
        {
            Year = year;
            Quarter = quarter;
            Month = month;
            Value = value;
        }

        public static List<HierarchicalData> GetData()
        {
            var data = new List<HierarchicalData>();
            var times = new string[4, 3] { { "Jan", "Feb", "Mar" }, { "Apr", "May", "June" }, { "Jul", "Aug", "Sep" }, { "Oct", "Nov", "Dec" } };
            var years = new int[] { 2015, 2016, 2017 };
            for (int i = 0; i < years.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string quar = "Q" + (j + 1);
                        if (j % 2 == 0)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                data.Add(new HierarchicalData(years[i], quar, times[j, k], 100));
                            }
                        }
                        else
                        {
                            data.Add(new HierarchicalData(years[i], quar, null, 100));
                        }
                    }
                }
                else
                {
                    data.Add(new HierarchicalData(years[i], null, null, 100));
                }
            }
            return data;
        }
    }
}