using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultipleControlsBinding.Models
{
    public class Fruit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MarPrice { get; set; }
        public int AprPrice { get; set; }
        public int MayPrice { get; set; }

        public string Country { get; set; }

        private static List<string> COUNTRIES = new List<string> { "US", "UK", "Canada", "Japan", "China", "France", "German", "Italy", "Korea", "Australia" };
        public static IEnumerable<Fruit> GetFruitsSales()
        {
            var rand = new Random(0);
            var fruits = new[] { "Oranges", "Apples", "Pears", "Bananas", "Pineapples",
                "Peaches", "Strawberries", "Cherries", "Grapes", "Mangos", "Lemons"};
            var list = fruits.Select((f, i) =>
            {
                int id = i + 1;
                int mar = rand.Next(1, 6);
                int apr = rand.Next(1, 9);
                int may = rand.Next(1, 6);
                var country = COUNTRIES[rand.Next(0, COUNTRIES.Count - 1)];
                return new Fruit { ID = id, Country = country, Name = f, MarPrice = mar, AprPrice = apr, MayPrice = may };
            });

            return list;
        }
    }
}