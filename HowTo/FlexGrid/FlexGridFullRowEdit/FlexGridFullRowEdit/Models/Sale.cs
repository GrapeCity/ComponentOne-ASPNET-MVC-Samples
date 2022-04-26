using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexGridFullRowEdit.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string Color { get; set; }
        public double Amount { get; set; }
        public double Amount2 { get; set; }
        public double Discount { get; set; }
        public bool Active { get; set; }

        public MonthData[] Trends { get; set; }
        public int Rank { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        private static List<string> PRODUCTS = new List<string> { "Widget", "Gadget", "Doohickey" };

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var colors = new[] { "Black", "White", "Red", "Green", "Blue" };
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = PRODUCTS[rand.Next(0, PRODUCTS.Count - 1)];
                var color = colors[rand.Next(0, colors.Length - 1)];
                var startDate = new DateTime(dt.Year, i % 12 + 1, 25);
                var endDate = new DateTime(dt.Year, i % 12 + 1, 25, i % 24, i % 60, i % 60);

                return new Sale
                {
                    ID = i + 1,
                    Start = startDate,
                    End = endDate,
                    Country = country,
                    Product = product,
                    Color = color,
                    Amount = Math.Round(rand.NextDouble() * 10000 - 5000, 2),
                    Amount2 = Math.Round(rand.NextDouble() * 10000 - 5000, 2),
                    Discount = Math.Round(rand.NextDouble() / 4, 2),
                    Active = (i % 4 == 0),
                    Trends = Enumerable.Range(0, 12).Select(x => new MonthData { Month = x + 1, Data = rand.Next(0, 100) }).ToArray(),
                    Rank = rand.Next(1, 6)
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

    public class MonthData
    {
        public int Month { get; set; }
        public double Data { get; set; }
    }
}