using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcExplorer.Models
{
    public class SaleShowCase : Sale
    {
        public ProductDetail ProductDetail { get {
                return ProductDetail.GetByName(Product);
            } }

        public string CountryFlag { get { return FullCountry.GetCountry(Country).Flag; } }

        public static SaleShowCase FromSale(Sale sale){
            return new SaleShowCase()
            {
                ID = sale.ID,
                Start = sale.Start,
                End = sale.End,
                Country = sale.Country,
                Product = sale.Product,
                Color = sale.Color,
                Amount = sale.Amount,
                Amount2 = sale.Amount2,
                Discount = sale.Discount,
                Active = sale.Active,
                Url = sale.Url,
                Img = sale.Img,
                History = sale.History,
                Trends = sale.Trends,
                Rank = sale.Rank
            };
        }
    }

    public class ProductDetail
    {
        public int Size { get; set; }
        public int Weight { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }

        public static ProductDetail GetByName(string name)
        {
            Random ran = new Random();
            return new ProductDetail
            {
                Size = ran.Next(100, 1000),
                Weight = ran.Next(1200, 10000),
                Quantity = ran.Next(5, 59),
                Description = string.Format("Description for {0}", name)
            };
        }
    }

    public class ColorObject
    {
        public string Name { get; set; }  
        public static List<ColorObject> GetColorObjects()
        {                                           
            return Sale.GetColors().Select(x => new ColorObject() { Name = x }).ToList();
        }
    }

    public class ProductObject
    {
        public string Name { get; set; }
        public static List<ProductObject> GetProductObjects()
        {
            return Sale.GetProducts().Select(x => new ProductObject() { Name = x }).ToList();
        }
    }
}