using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcExplorer.Models
{
    public class SalesData
    {
        public string CountryName { get; set; }
        public int Sale { get; set; }

        public SalesData(string name, int sale)
        {
            CountryName = name;
            Sale = sale;
        }

        public static List<SalesData> GetData()
        {
            var names = new string[] { "US", "Germany", "UK", "Japan", "Italy", "Greece" };
            int sales = 10000;
            List<SalesData> data = new List<SalesData>();
            for (int i = 0, len = names.Length; i < len; i++)
            {
                sales = sales - (i * 500);
                data.Add(new SalesData(names[i], sales));
            };
            return data;
        }
    }
}