using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class ProductSales
    {
        public string Country { get; set; }
        public int Downloads { get; set; }
        public int Sales { get; set; }
        public int Refunds { get; set; }
        public int Damages { get; set; }
        public static List<ProductSales> GetData()
        {
            var countries = "US,Germany,UK,Japan,Italy,Greece".Split(new char[] { ',' });
            var data = new List<ProductSales>();
            for (var i = 0; i < countries.Length; i++)
            {
                data.Add(new ProductSales()
                {
                    Country = countries[i],
                    Downloads = ((i % 4) * 40) + 20,
                    Sales = ((i % 7) * 25) + 20,
                    Refunds = ((i % 3) * 45) + 20,
                    Damages = ((i % 9) * 20) + 20
                });
            }
            return data;
        }
    }
}