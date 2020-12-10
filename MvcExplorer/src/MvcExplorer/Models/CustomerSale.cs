using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExplorer.Models
{
    public class CustomerSale
    {
        public static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        public static List<NamedProduct> PRODUCTS = new List<NamedProduct> {
            new NamedProduct {Id = "1", Name = "Widget"},
            new NamedProduct {Id = "2", Name = "Gadget"},
            new NamedProduct {Id = "3", Name = "Doohickey"}
        };

        public static List<ProductColor> COLORS = new List<ProductColor> {
            new ProductColor {Name = "Black", Value = "#000000"},
            new ProductColor {Name = "White", Value = "#FFFFFF"},
            new ProductColor {Name = "Red", Value = "#FF0000"},
            new ProductColor {Name = "Green", Value = "#008000"},
            new ProductColor {Name = "Cyan", Value = "#00FFFF"},
            new ProductColor {Name = "DarkBlue", Value = "#0000A0"},
            new ProductColor {Name = "LightBlue", Value = "#ADD8E6"},
            new ProductColor {Name = "Purple", Value = "#800080"},
            new ProductColor {Name = "Yellow", Value = "#FFFF00"},
            new ProductColor {Name = "Lime", Value = "#00FF00"},
            new ProductColor {Name = "Magenta", Value = "#FF00FF"},
            new ProductColor {Name = "Olive", Value = "#808000"},
            new ProductColor {Name = "Maroon", Value = "#800000"},
            new ProductColor {Name = "Brown", Value = "#A52A2A"},
            new ProductColor {Name = "Orange", Value = "#FFA500"},
            new ProductColor {Name = "Gray", Value = "#808080"},
            new ProductColor {Name = "Silver", Value = "#C0C0C0"},
            new ProductColor {Name = "Night", Value = "#0C090A"},
            new ProductColor {Name = "Gunmetal", Value = "#2C3539"},
            new ProductColor {Name = "Midnight", Value = "#2B1B17"},
            new ProductColor {Name = "Charcoal", Value = "#34282C"},
            new ProductColor {Name = "Oil", Value = "#3B3131"},
            new ProductColor {Name = "Black Cat", Value = "#413839"},
            new ProductColor {Name = "Iridium", Value = "#3D3C3A"},
            new ProductColor {Name = "Columbia Blue", Value = "#87AFC7"},
            new ProductColor {Name = "Teal", Value = "#008080"},
            new ProductColor {Name = "Venom Green", Value = "#728C00"},
            new ProductColor {Name = "Blue", Value = "#0000FF"}
        };

        public static List<NamedCountry> NAMEDCOUNTRIES = COUNTRIES.Select(country =>
        {
            return new NamedCountry { Name = country };
        }).ToList();

        public static List<NamedRank> RANKS = Enumerable.Range(1, 5).Select(i =>
        {
            return new NamedRank { Name = i };
        }).ToList();

        public static IEnumerable<Sale> GetData(int total)
        {
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = PRODUCTS[rand.Next(0, PRODUCTS.Count - 1)].Id;
                var color = COLORS[rand.Next(0, COLORS.Count - 1)].Value;
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

        public class NamedCountry
        {
            public string Name { get; set; }
        }

        public class NamedProduct
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }

        public class ProductColor
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }

        public class NamedRank
        {
            public int Name { get; set; }
        }
    }
}
