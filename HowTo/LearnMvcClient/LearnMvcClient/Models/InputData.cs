using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnMvcClient.Models
{
    public static class InputData
    {
        public class Sale
        {
            public string Country { get; set; }
            public bool Active { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
        }

        private static string[] _countries = new[] { "China", "Germany", "Greece", "Italy", "Japan", "Portugal", "Russia", "Spain", "UK", "US" };
        private static IEnumerable<Sale> _sales;

        public static IEnumerable<string> GetCountries()
        {
            return _countries;
        }

        public static IEnumerable<Sale> GetSales()
        {
            return _sales ?? (_sales = GetSalesData(_countries));
        }
        private static IEnumerable<Sale> GetSalesData(string[] countries)
        {
            var rand = new Random(0);
            var list = new List<Sale>();
            for(var i = 0; i < countries.Length; i++)
            {
                list.Add(new Sale
                {
                    Country = countries[i],
                    Active = i % 5 != 0,
                    Sales = rand.NextDouble() * 100000,
                    Expenses = rand.NextDouble() * 50000
                });
            }
            return list;
        }
    }
}