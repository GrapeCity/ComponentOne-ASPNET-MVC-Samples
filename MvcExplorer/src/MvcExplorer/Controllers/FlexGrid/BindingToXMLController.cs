using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc.Serialization;
using C1.Web.Mvc;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        public ActionResult BindingToXML()
        {
            return View();
        }

#if NETCORE31
        public ActionResult GetProductsByCategory([C1JsonRequest] CollectionViewRequest<TCategory> requestData, [FromServices] IWebHostEnvironment environment)
#else
        public ActionResult GetProductsByCategory([C1JsonRequest] CollectionViewRequest<TCategory> requestData, [FromServices] IHostingEnvironment environment)
#endif
        {
            var items = new List<TCategory>();
            var xml = XElement.Load(Path.Combine(environment.WebRootPath, "Content/data/ProductCategories.xml"));
            // get categories
            var categories = xml.Elements("category");
            foreach (var cg in categories)
            {
                items.Add(new TCategory
                {
                    Id = Convert.ToInt32(cg.Attribute("id").Value),
                    Name = cg.Attribute("name").Value,
                    Products = new List<TProduct>()
                });
                // get products in this category
                var products = cg.Elements("product");
                foreach (var p in products)
                {
                    items[items.Count - 1].Products.Add(new TProduct
                    {
                        Id = Convert.ToInt32(p.Attribute("id").Value),
                        Name = p.Attribute("name").Value,
                        Price = Convert.ToDouble(p.Attribute("price").Value)
                    });
                }
            }
            return this.C1Json(CollectionViewHelper.Read(requestData, items));
        }

        public class TCategory
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<TProduct> Products { get; set; }
        }

        public class TProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
    }
}
