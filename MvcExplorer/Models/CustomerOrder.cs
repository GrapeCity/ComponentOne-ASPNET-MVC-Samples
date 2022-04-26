using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MvcExplorer.Models
{
    public class CustomerOrder
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Country { get; set; }
        public string Product { get; set; }
        public DateTime OrderTime { get; set; }

        [Range(1,10)]
        public int Count { get; set; }

        [Range(10,500)]
        public decimal Price { get; set; }

        /// <summary>
        /// total = 0 means one order in one country
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<CustomerOrder> GetOrderData(int total)
        {
            var countries = new[] { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
            var products = new[] { "PlayStation 4", "XBOX ONE", "Wii U", "PlayStation Vita", "PlayStation 3", "XBOX 360" };
            var rand = new Random(0);
            var baseTime = DateTime.Parse("2014-1-1");
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = countries[rand.Next(0, countries.Length - 1)];
                var product = products[rand.Next(0, products.Length - 1)];
                var time = baseTime.AddSeconds(rand.Next(600, 36000));
                baseTime = time;
                var count = rand.Next(1, 5);
                return new CustomerOrder { ID = i + 1, Country = country, Product = product, OrderTime = time, Count = count };
            });
            return list;
        }

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

        public static IEnumerable<CustomerOrder> GetCountryCountOrderData(int rangeMin, int rangeMax)
        {
            var countries = new[] { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
            var rand = new Random(0);
            List<CustomerOrder> list = new List<CustomerOrder>();
            for (int i = 0; i < 10; i++)
            {
                var count = rand.Next(rangeMin, rangeMax);
                list.Add(new CustomerOrder { ID = i + 1, Count = count, Country = countries[i] });
            }
            return list;
        }

        public static IEnumerable<CustomerOrder> GetCountryNameData()
        {
            var countries = new[] { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
            var list = countries.Select((c, i) =>
            {
                return new CustomerOrder { ID = i + 1, Country = c };
            });
            return list;
        }
    }
}