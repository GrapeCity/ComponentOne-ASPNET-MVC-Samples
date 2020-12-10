using System;
using System.Collections.Generic;
using System.Linq;
namespace FlexSheetExplorer.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "Germany", "UK", "Japan", "Italy", "Greece" };
        private static List<string> PRODUCTS = new List<string> { "Widget", "Gadget", "Doohickey" };

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var rand = new Random(0);
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = PRODUCTS[rand.Next(0, PRODUCTS.Count - 1)];
                var date = new DateTime(2014, i % 12 + 1, 25);

                return new Sale
                {
                    ID = i + 1,
                    Date = date,
                    Country = country,
                    Product = product,
                    Amount = Math.Round(rand.NextDouble() * 10000 - 5000, 2),
                    Active = (i % 4 == 0)
                };
            });
            return list;
        }

        public static List<string> GetCountries()
        {
            var countries = new List<string>();
            countries.AddRange(COUNTRIES);
            return countries;
        }

        public static List<string> GetProducts()
        {
            List<string> products = new List<string>();
            products.AddRange(PRODUCTS);
            return products;
        }
    }
}