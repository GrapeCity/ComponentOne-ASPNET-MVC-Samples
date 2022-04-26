using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EditableAnnotationLayer.Models
{
    public class CountryData
    {
        public decimal? x { get; set; }
        public decimal? y { get; set; }

        /// <summary>
        /// To generate some random data for FlexChart
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<CountryData> GetCountryData()
        {
            List<CountryData> list = new List<CountryData>();
            var rand = new Random(0);
            for (int i = 1; i <= 100; i++)
            {
                list.Add(new CountryData { x = rand.Next(100), y = rand.Next(1000) });
            }
            return list;
        }

    }
}