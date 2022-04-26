using System;
using System.Collections.Generic;

namespace Olap101.Models
{
    public class ProductData
    {
        private static Random r = new Random();

        public int ID { get; set; }
        public string Product { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int Sales { get; set; }
        public int Downloads { get; set; }
        public bool Active { get; set; }
        public double Discount { get; set; }

        private static int randomInt(int max)
        {
            return (int)Math.Floor(r.NextDouble() * (max + 1));
        }

        public static IEnumerable<ProductData> GetData(int cnt)
        {
            string[] countries = "China,India,Russia,US,Germany,UK,Japan,Italy,Greece,Spain,Portugal".Split(',');
            string[] products = "Wijmo,Aoba,Xuni,Olap".Split(',');
            List<ProductData> result = new List<ProductData>();
            for (var i = 0; i < cnt; i++)
            {
                result.Add(new ProductData
                {
                    ID = i,
                    Product = products[randomInt(products.Length - 1)],
                    Country = countries[randomInt(countries.Length - 1)],
                    Date = new DateTime(DateTime.Today.Year - 2 + randomInt(2), randomInt(11) + 1, randomInt(27) + 1),
                    Sales = randomInt(10000),
                    Downloads = randomInt(10000),
                    Active = randomInt(1) == 1 ? true : false,
                    Discount = r.NextDouble()
                });
            }
            return result;
        }
    }
}