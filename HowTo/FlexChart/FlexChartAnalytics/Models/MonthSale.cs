using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChartAnalytics.Models
{
    public class MonthSale
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public static IEnumerable<MonthSale> GetData()
        {
            var rand = new Random();
            var names = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var data = new List<MonthSale>(12);
            for (int i = 0, len = names.Length; i < len; i++)
            {
                data.Add(new MonthSale() { Name = names[i], Value = rand.Next(-500, 500) });
            }

            return data;
        }
    }
}