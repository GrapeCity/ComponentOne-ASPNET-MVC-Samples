using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheet101.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public string CountryId { get; set; }
        public string ProductId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; }


        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var countries = new[] { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
            var products = new[] { "Widget", "Gadget", "Doohickey" };
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = countries[rand.Next(0, countries.Length - 1)];
                var product = products[rand.Next(0, products.Length - 1)];
                var date = new DateTime(dt.Year, i % 12 + 1, 25, i % 24, i % 60, i % 60);

                return new Sale
                {
                    ID = i + 1,
                    CountryId = country,
                    ProductId= product,
                    Date = date,
                    Amount = rand.NextDouble() * 10000 - 5000,                    
                    Active = (i % 4 == 0),
                };
            });
            return list;
        }
    }
    
}