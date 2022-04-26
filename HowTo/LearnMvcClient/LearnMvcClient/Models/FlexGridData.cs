using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMvcClient.Models
{
    public class FlexGridData
    {
        public class Sale
        {
            public int Id { get; set; }
            public string Country { get; set; }
            public string Product { get; set; }
            public bool Active { get; set; }
            public int Downloads { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
            public bool Overdue { get; set; }
            public string Customer { get; set; }
        }

        public static string[] Countries4 = new[] { "US", "Germany", "UK", "Japan" };
        public static string[] Countries6 = new[] { "US", "Germany", "UK", "Japan", "Italy", "Greece" };
        public static string[] Countries7 = new[] { "US", "Germany", "UK", "Japan", "Sweden", "Norway", "Denmark" };
        private static string[] Products = new[] { "Phones", "Computers", "Cameras", "Stereos" };
        private static string[] Customers = new[] { "Paul Smith", "Susan Johnson" };

        public static IEnumerable<Sale> GetSales()
        {
            return GetSales(Countries6);
        }

        public static IEnumerable<Sale> GetSales(int count)
        {
            return GetSales(Countries6, count);
        }

        public static IEnumerable<Sale> GetSales(string[] countries)
        {
            return GetSales(countries, countries.Length);
        }

        public static IEnumerable<Sale> GetSales(string[] countries, int count)
        {
            var rand = new Random(0);
            var list = new List<Sale>();
            for(int i = 0; i < count; i++)
            {
                list.Add(new Models.FlexGridData.Sale
                {
                    Id = i,
                    Country = countries[i % countries.Length],
                    Product = Products[i % Products.Length],
                    Active = i % 5 != 0,
                    Downloads = rand.Next(200000),
                    Sales = rand.NextDouble() * 100000,
                    Expenses = rand.NextDouble() * 50000,
                    Overdue = i % 4 == 0,
                    Customer = i < count / 2 ? Customers[0] : Customers[1]
                });
            }
            return list;
        }
    }
}