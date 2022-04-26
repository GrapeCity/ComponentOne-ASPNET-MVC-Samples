using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexPie101.Models
{
    public class CustomerOrder
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public DateTime OrderTime { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// total = 0 means one order in one country
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>       

        public static IEnumerable<CustomerOrder> GetCountryGroupOrderData()
        {
            var countries = new[] { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
            var products = new[] { "PlayStation 4", "XBOX ONE", "Wii U", "PlayStation Vita", "PlayStation 3", "XBOX 360" };
            var rand = new Random(0);
            var baseTime = DateTime.Parse("2014-1-1");
            var list = countries.Select((country, i) =>
            {
                var product = products[rand.Next(0, products.Length - 1)];
                var time = baseTime.AddSeconds(rand.Next(600, 36000));
                baseTime = time;
                var count = rand.Next(1, 5);
                var price = rand.Next(99, 499);
                return new CustomerOrder { ID = i + 1, Country = country, Product = product, OrderTime = time, Count = count, Price = price };
            });
            return list;
        }
        
    }
}