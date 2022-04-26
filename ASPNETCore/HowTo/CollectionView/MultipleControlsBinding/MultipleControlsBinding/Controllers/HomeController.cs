using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;
using MultipleControlsBinding.Models;

namespace MultipleControlsBinding.Controllers
{
    public class HomeController : Controller
    {
        public static List<Fruit> fruitsSales = Fruit.GetFruitsSales().ToList();
        public ActionResult Index()
        {
            var settings = new Dictionary<string, object[]>
            {
                {"ChartType", new object[]{"Column", "Bar", "Scatter", "Line", "LineSymbols", "Area", "Spline", "SplineSymbols", "SplineArea"}},
                {"Rotated", new object[]{"False", "True"}}
            };
            ViewBag.Settings = settings;
            return View(fruitsSales);
        }

        public ActionResult Update([C1JsonRequest]CollectionViewEditRequest<Fruit> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Fruit>(requestData, fruit =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = fruitsSales.Find(item => item.ID == fruit.ID);
                fSale.Name = fruit.Name;
                fSale.Country = fruit.Country;
                fSale.MayPrice = fruit.MayPrice;
                fSale.MarPrice = fruit.MarPrice;
                fSale.AprPrice = fruit.AprPrice;
                return new CollectionViewItemResult<Fruit>
                {
                    Error = error,
                    Success = success,
                    Data = fSale
                };
            }, () => fruitsSales));
        }

        public ActionResult Create([C1JsonRequest]CollectionViewEditRequest<Fruit> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Fruit>(requestData, fruit =>
            {
                string error = string.Empty;
                bool success = true;
                fruit.ID = GetId();
                fruitsSales.Add(fruit);
                return new CollectionViewItemResult<Fruit>
                {
                    Error = error,
                    Success = success,
                    Data = fruit
                };
            }, () => fruitsSales));
        }

        public ActionResult Delete([C1JsonRequest]CollectionViewEditRequest<Fruit> requestData)
        {
            return this.C1Json(CollectionViewHelper.Edit<Fruit>(requestData, fruit =>
            {
                string error = string.Empty;
                bool success = true;
                var fSale = fruitsSales.Find(item => item.ID == fruit.ID);
                fruitsSales.Remove(fSale);
                return new CollectionViewItemResult<Fruit>
                {
                    Error = error,
                    Success = success,
                    Data = fruit
                };
            }, () => fruitsSales));
        }

        private int GetId()
        {
            int id = 1;
            while (fruitsSales.Find(item => item.ID == id) != null)
            {
                id++;
            }
            return id;
        }
    }
}
