using MvcExplorer.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace MvcExplorer.Models
{
    public class Sale
    {
        public Sale(){
            Start = End = DateTime.Now;
        }
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Start_Required_ErrorMessage")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Start_DataType_ErrorMessage")]
        public DateTime Start { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "EndTime_Required_ErrorMessage")]
        public DateTime End { get; set; }
        public string Country { get; set; }
        public string Product { get; set; }
        public string Color { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Change_Required_ErrorMessage")]
        public double Amount { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Price_Required_ErrorMessage")]
        [RegularExpression(pattern: @"^\$?[0-9]\d*(\.\d+)?$", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Price_DataType_ErrorMessage")]
        public double Amount2 { get; set; }
        [Required(ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Discount_Required_ErrorMessage")]
        [RegularExpression(pattern: @"^[0-9]\d*(\.\d+)?\%?$", ErrorMessageResourceType = typeof(ValidationRes), ErrorMessageResourceName = "Discount_DataType_ErrorMessage")]
        public double Discount { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public int[] History { get; set; }

        public MonthData[] Trends { get; set; }
        public int Rank { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        private static List<string> PRODUCTS = new List<string> { "Widget", "Gadget", "Doohickey" };
        private static List<string> COLORS = new List<string> { "Black", "White", "Red", "Green", "Blue" };

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<Sale> GetData(int total)
        {
            var rand = new Random(0);
            var dt = DateTime.Now;
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                var product = PRODUCTS[rand.Next(0, PRODUCTS.Count - 1)];
                var color = COLORS[rand.Next(0, COLORS.Count - 1)];
                var startDate = new DateTime(dt.Year, i % 12 + 1, 25);
                var endDate = new DateTime(dt.Year, i % 12 + 1, 25, i % 24, (i % 2) * 30, 0);

                return new Sale
                {
                    ID = i + 1,
                    Start = startDate,
                    End = endDate,
                    Country = country,
                    Product = product,
                    Color = color,
                    Amount = Math.Round(rand.NextDouble() * 10000 - 5000, 2),
                    Amount2 = Math.Round(rand.NextDouble() * 5000, 2),
                    Discount = Math.Round(rand.NextDouble() / 4, 2),
                    Active = (i % 4 == 0),
                    Trends = Enumerable.Range(0, 12).Select(x => new MonthData { Month = x + 1, Data = rand.Next(0, 100) }).ToArray(),
                    Rank = rand.Next(1, 6),
                    Url = string.Format("https://en.wikipedia.org/wiki/Tourism_in_{0}", country),
                    Img = string.Format("https://cdn.grapecity.com/wijmo/images/{0}.png", i % 4 + 1),
                    History = GetHistory(3 + rand.Next(20), 100)
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

        public static List<string> GetColors()
        {
            List<string> colors = new List<string>();
            colors.AddRange(COLORS);
            return colors;
        }

        public static IEnumerable<Sale> GetValidationData()
        {
            var data = GetData(5).ToList();
            var i = data.Max(s => s.ID);
            data.Add(new Sale
            {
                ID = ++ i,
                Start = DateTime.Now,
                End = DateTime.Now,
                Country = "Russia",
                Product = "Aoba",
                Color = "Black",
                Amount = 2000,
                Amount2 = 5000,
                Discount = 0.3,
                Active = (i % 4 == 0),
                Trends = Enumerable.Range(0, 12).Select(x => new MonthData { Month = x + 1, Data = 2 }).ToArray(),
                Rank = 2
            });
            data.Add(new Sale
            {
                ID = ++ i,
                Start = DateTime.Now,
                End = DateTime.Now,
                Country = "New Zealand",
                Product = "InputMan",
                Color = "Red",
                Amount = 2000,
                Amount2 = -10,
                Discount = 0.3,
                Active = (i % 4 == 0),
                Trends = Enumerable.Range(0, 12).Select(x => new MonthData { Month = x + 1, Data = 2 }).ToArray(),
                Rank = 2
            });
            return data;
        }

        private static int[] GetHistory(int cnt, int max)
        {
            var rand = new Random(0);
            int[] values = new int[cnt];
            for (int i = 0; i < cnt; i++)
            {
                values[i] = rand.Next(max);
            }
            return values;
        }
    }

    public class MonthData
    {
        public int Month { get; set; }
        public double Data { get; set; }
    }

    public class BasicSale
    {
        public int Sale { get; set; }
        public DateTime Date { get; set; }

        public BasicSale(int sale, DateTime date)
        {
            Sale = sale;
            Date = date;
        }
        public static List<BasicSale> GetBasicSales()
        {
            List<BasicSale> list = new List<BasicSale>();
            int[] sales = {
            96, 19, 54, 83, 15, 56, 36, 4, 29, 93,
            38, 71, 50, 77, 69, 13, 79, 57, 29, 62,
            4, 27, 66, 96, 65, 12, 52, 3, 61, 48, 50,
            70, 39, 33, 25, 49, 69, 46, 44, 40, 35,
            72, 64, 10, 66, 63, 78, 19, 96, 26};
            for (int i = 0; i < sales.Length; i++)
            {
                list.Add(new BasicSale(sales[i], new DateTime(2014, i / 31 + 1, i % 31 + 1)));
            }
            return list;
        }
    }

    public class FullCountry
    {
        private static List<FullCountry> COUNTRIES = new List<FullCountry> {
            new FullCountry{ Id = 0, Name = "US", Flag = "us" },
            new FullCountry{ Id = 1, Name = "UK", Flag = "gb" },
            new FullCountry{ Id = 2, Name = "Canada", Flag = "ca"},
            new FullCountry{ Id = 3, Name = "Japan", Flag = "jp" },
            new FullCountry{ Id = 4, Name = "China", Flag = "cn"},
            new FullCountry{ Id = 5, Name = "France", Flag = "fr"},
            new FullCountry{ Id = 6, Name = "German", Flag = "de"},
            new FullCountry{ Id = 7, Name = "Italy", Flag = "it"},
            new FullCountry{ Id = 8, Name = "Korea", Flag = "kr"},
            new FullCountry{ Id = 9, Name = "Australia", Flag = "au"}
        };
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public static List<FullCountry> GetCountries()
        {
            return COUNTRIES;
        }

        public static FullCountry GetCountry(string country)
        {
            return COUNTRIES.Find(x => x.Name.Equals(country));
        }
    }

}