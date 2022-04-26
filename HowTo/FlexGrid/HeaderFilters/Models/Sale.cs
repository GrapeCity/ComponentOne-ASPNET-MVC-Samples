using System;
using System.Collections.Generic;
using System.Linq;

namespace HeaderFilters.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public double Sales { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Rank { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var rand = new Random(0);
            var products = new string[] { "Widget", "Gadget", "Doohickey" };
            var countries = GetCountries();
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = products[rand.Next(0, products.Length - 1)];

                return new Sale
                {
                    ID = i + 1,
                    Country = country,
                    Product = product,
                    Sales = rand.NextDouble() * 10000 - 5000,
                    Price = rand.NextDouble() * 100,
                    Discount = rand.NextDouble() / 4,
                    Rank = rand.Next(1, 6)
                };
            });
            return list;
        }

        public static List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            countries.Add("(all countries)");
            countries.AddRange(COUNTRIES);
            return countries;
        }
    }
}