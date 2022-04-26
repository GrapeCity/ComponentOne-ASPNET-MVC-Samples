using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexSheetExplorer.Models
{
    public static class CustomerSale
    {
        public static List<NamedCountry> COUNTRIES = new List<NamedCountry> { 
            new NamedCountry{Id="1", Name="US"},
            new NamedCountry{Id="2", Name="Germany"},
            new NamedCountry{Id="3", Name="Japan"},
            new NamedCountry{Id="4", Name="Italy"},
            new NamedCountry{Id="5", Name="Greece"}
        };
        public static List<NamedProduct> PRODUCTS = new List<NamedProduct> {
            new NamedProduct {Id = "1", Name = "Widget"},
            new NamedProduct {Id = "2", Name = "Gadget"},
            new NamedProduct {Id = "3", Name = "Doohickey"}
        };

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
                    Country = country.Id,
                    Date =date,
                    Product = product.Id,
                    Amount = Math.Round(rand.NextDouble() * 10000 - 5000, 2),
                    Active = (i % 4 == 0)
                };
            });
            return list;
        }
    }

    public class NamedProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class NamedCountry
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}