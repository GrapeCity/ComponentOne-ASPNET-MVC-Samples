using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowExplorer.Models
{
    public class DataRepresentation
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public string ytd { get; set; }
        public string m1 { get; set; }
        public string m6 { get; set; }
        public string m12 { get; set; }
        public string stock { get; set; }
        public string bond { get; set; }
        public string cash { get; set; }
        public string other { get; set; }

        private static List<string> NAME = new List<string> { "Constant Growth IXTR", "Optimus Prime MMCT", "Serenity Now ZTZZZ" };
        private static List<string> CURRENCY = new List<string> { "USD", "EUR", "YEN" };

        public static IEnumerable<DataRepresentation> GetData(int total)
        {
            var rand = new Random(0);
            var list = Enumerable.Range(0, total).Select(i =>
            {
                var name = NAME[rand.Next(0, NAME.Count - 1)];
                var currency = CURRENCY[rand.Next(0, CURRENCY.Count - 1)];

                return new DataRepresentation
                {
                    Name = name,
                    Currency = currency,
                    ytd = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    m1 = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    m6 = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    m12 = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    stock = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    bond = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    cash = Math.Round(rand.NextDouble() * 10, 2) + "%",
                    other = Math.Round(rand.NextDouble() * 10, 2) + "%"
                };
            });
            return list;
        }
    }
}