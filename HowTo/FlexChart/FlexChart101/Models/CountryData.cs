using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexChart101.Models
{
    public class CountryData
    {
        public int ID { get; set; }
        public string Country { get; set; }        
        public decimal Downloads { get; set; }
        public decimal Sales { get; set; }
        public decimal Expenses { get; set; }

        /// <summary>
        /// total = 0 means one order in one country
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>       
        /// // generate some random data
        public static IEnumerable<CountryData> GetCountryData()
        {
            var countries = new[] { "US","Germany","UK","Japan","Italy","Greece" };
            var rand = new Random(0);
            var baseTime = DateTime.Parse("2015-1-1");
            var list = countries.Select((country, i) =>
            {
                var download = rand.Next(500, 1000) * 20000;
                var sale = rand.Next(100, 1000) * 10000;
                var expense = rand.Next(100, 800) * 5000;
                return new CountryData { ID = i + 1, Country = country, Downloads = download, Sales = sale, Expenses = expense };
            });
            return list;
        }

    }
}