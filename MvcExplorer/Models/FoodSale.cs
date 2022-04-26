using System;
using System.Collections.Generic;

namespace MvcExplorer.Models
{
    public class FoodSale
    {
        private static List<string> Categories = new List<string> { "Beverages", "Condiments", "Confections", "Dairy Products", "Grains/Cereals", "Meat/Poultry", "Produce", "Seafood" };
        private static List<string[]> SubCategories = new List<string[]>
        {
            new string[] { "Soft drinks", "Coffees", "Teas", "Beers", "Ales" },
            new string[] { "Sweet and Savory sauces", "Relishes", "Spreads", "Seasonings" },
            new string[] { "Desserts", "Candies", "Sweet breads" },
            new string[] { "Chesses" },
            new string[] { "Breads", "Crackers", "Pasta", "Cereal" },
            new string[] { "Prepared meats" },
            new string[] { "Dried fruit", "Bean curd" },
            new string[] { "Seaweed", "Fish" }
        };

        public string Category { get; set; }
        public string SubCategory { get; set; }
        public double Sales { get; set; }

        public static IEnumerable<FoodSale> GetDate()
        {
            var result = new List<FoodSale>();
            var rand = new Random(0);
            var index = 0;
            foreach(var cat in Categories)
            {
                var subCategories = SubCategories[index++];
                foreach(var subCat in subCategories)
                {
                    result.Add(new FoodSale
                    {
                        Category = cat,
                        SubCategory = subCat,
                        Sales = rand.NextDouble() * 100
                    });
                }
            }

            return result;
        }

        public static IEnumerable<FoodSale> GetGroupData()
        {
            var result = new List<FoodSale>();
            var rand = new Random(0);
            var catLen = Categories.Count;
            for(var i=0; i<1000; i++)
            {
                var randomC = rand.Next(0, catLen - 1);
                var subCat = SubCategories[randomC];
                var randomSC = rand.Next(0, subCat.Length - 1);
                result.Add(new FoodSale
                {
                    Category = Categories[randomC],
                    SubCategory = subCat[randomSC],
                    Sales = rand.NextDouble() * 100
                });
            }
            return result;
        }
    }
}