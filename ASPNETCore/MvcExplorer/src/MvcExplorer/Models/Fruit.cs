using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class Fruit
    {
        public string Name { get; set; }

        public int MarPrice { get; set; }
        public int AprPrice { get; set; }
        public int MayPrice { get; set; }

        private IEnumerable<FruitSale> _sales = null;
        public IEnumerable<FruitSale> Sales
        {
            get 
            {
                if (_sales == null)
                {
                    _sales = GetSales();
                }
                return _sales;
            }
        }

        public static IEnumerable<Fruit> GetFruitsSales()
        {
            var rand = new Random(0);
            var fruits = new[] { "Oranges", "Apples", "Pears", "Bananas", "Pineapples" };
            var list = fruits.Select((f, i) =>
            {
                int mar = rand.Next(1, 6);
                int apr = rand.Next(1, 9);
                int may = rand.Next(1, 6);
                return new Fruit { Name = f, MarPrice = mar, AprPrice = apr, MayPrice = may };
            });

            return list;
        }

        private IEnumerable<FruitSale> GetSales()
        {
            var rand = new Random(0);
            var today = DateTime.Now.Date;
            var firstDay = new DateTime(today.Year - 1, 3, 1);
            var dataTimes = new List<DateTime>();
            for (int i = 0; i < 92; i++) 
            {
                dataTimes.Add(firstDay.AddDays(i+1));
            }
            var list = dataTimes.Select((date, i) => {
                FruitSale sale = new FruitSale { Date = date };
                sale.SalesInChina = rand.Next(150, 250);
                if (i % 30 != 0)
                {
                    sale.SalesInUSA = rand.Next(100, 200);
                    sale.SalesInJapan = rand.Next(0, 100);
                }
                else 
                {
                    sale.SalesInUSA = null;
                    sale.SalesInJapan = null;
                }

                return sale;
            });

            return list;
        }
    }

    public class FruitSale 
    {
        public DateTime Date { get; set; }
        public int? SalesInUSA { get; set; }
        public int? SalesInChina { get; set; }
        public int? SalesInJapan { get; set; }
    }
}