using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFlexChartAxisScrollbar.Models
{
    public class CountryData
    {
        public DateTime Date { get; set; }
        public decimal? YVal { get; set; }
        

        /// <summary>
        /// To generate some random data for FlexChart
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CountryData> GetCountryData()
        {
            DateTime baseDate = new DateTime(2005, 1, 1);
            List<CountryData> list = new List<CountryData>();
            var rand = new Random(0);
            for (int i = 1; i <= 3000; i++)
            {
                var mod = Math.Floor((decimal)i / 10) % 10;
                decimal? val = null;
                if (mod != 0)
                    val= rand.Next(10) * 10;
                list.Add(new CountryData { Date = baseDate.AddDays(i), YVal = val });
            }
            return list;
        }

    }
}