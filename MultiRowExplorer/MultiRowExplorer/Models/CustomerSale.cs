using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowExplorer.Models
{
    public class CustomerSale
    {
        public static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        public static List<NamedProduct> PRODUCTS = new List<NamedProduct> {
            new NamedProduct {Id = "1", Name = "Widget"},
            new NamedProduct {Id = "2", Name = "Gadget"},
            new NamedProduct {Id = "3", Name = "Doohickey"}
        };

        public static List<NamedColor> COLORS = new List<NamedColor> {
            new NamedColor {Name = "Black", Value = "#000000"},
            new NamedColor {Name = "White", Value = "#FFFFFF"},
            new NamedColor {Name = "Red", Value = "#FF0000"},
            new NamedColor {Name = "Green", Value = "#008000"},
            new NamedColor {Name = "Cyan", Value = "#00FFFF"},
            new NamedColor {Name = "DarkBlue", Value = "#0000A0"},
            new NamedColor {Name = "LightBlue", Value = "#ADD8E6"},
            new NamedColor {Name = "Purple", Value = "#800080"},
            new NamedColor {Name = "Yellow", Value = "#FFFF00"},
            new NamedColor {Name = "Lime", Value = "#00FF00"},
            new NamedColor {Name = "Magenta", Value = "#FF00FF"},
            new NamedColor {Name = "Olive", Value = "#808000"},
            new NamedColor {Name = "Maroon", Value = "#800000"},
            new NamedColor {Name = "Brown", Value = "#A52A2A"},
            new NamedColor {Name = "Orange", Value = "#FFA500"},
            new NamedColor {Name = "Gray", Value = "#808080"},
            new NamedColor {Name = "Silver", Value = "#C0C0C0"},
            new NamedColor {Name = "Night", Value = "#0C090A"},
            new NamedColor {Name = "Gunmetal", Value = "#2C3539"},
            new NamedColor {Name = "Midnight", Value = "#2B1B17"},
            new NamedColor {Name = "Charcoal", Value = "#34282C"},
            new NamedColor {Name = "Oil", Value = "#3B3131"},
            new NamedColor {Name = "Black Cat", Value = "#413839"},
            new NamedColor {Name = "Iridium", Value = "#3D3C3A"},
            new NamedColor {Name = "Columbia Blue", Value = "#87AFC7"},
            new NamedColor {Name = "Teal", Value = "#008080"},
            new NamedColor {Name = "Venom Green", Value = "#728C00"},
            new NamedColor {Name = "Blue", Value = "#0000FF"}
        };

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

        public class NamedProduct
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}