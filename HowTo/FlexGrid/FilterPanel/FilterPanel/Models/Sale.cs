using System;
using System.Collections.Generic;
using System.Linq;

namespace FilterPanel.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public bool Active { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        private static List<string> PRODUCTS = new List<string> { "Widget", "Gadget", "Doohickey" };

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = PRODUCTS[rand.Next(0, PRODUCTS.Count - 1)];
                var startDate = new DateTime(dt.Year, i % 12 + 1, 25);

                return new Sale
                {
                    ID = i + 1,
                    Start = startDate,
                    Country = country,
                    Product = product,
                    Active = (i % 4 == 0)
                };
            });
            return list;
        }
    }
}