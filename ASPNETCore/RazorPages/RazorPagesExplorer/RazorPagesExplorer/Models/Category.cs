using System.Collections.Generic;

namespace RazorPagesExplorer.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public static List<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category(){ CategoryID=1, CategoryName="Beverages", Description="Soft drinks, coffees, teas, beers, and ales" },
                new Category(){ CategoryID=2, CategoryName="Condiments", Description="Sweet and savory sauces, relishes, spreads, and seasonings" },
                new Category(){ CategoryID=3, CategoryName="Confections", Description="Desserts, candies, and sweet breads" },
                new Category(){ CategoryID=4, CategoryName="Dairy Products", Description="Cheeses" },
                new Category(){ CategoryID=5, CategoryName="Grains/Cereals", Description="Breads, crackers, pasta, and cereal" },
                new Category(){ CategoryID=6, CategoryName="Meat/Poultry", Description="Prepared meats" },
                new Category(){ CategoryID=7, CategoryName="Produce", Description="Dried fruit and bean curd" },
                new Category(){ CategoryID=8, CategoryName="Seafood", Description="Seaweed and fish" },
            };
        }
    }
}
