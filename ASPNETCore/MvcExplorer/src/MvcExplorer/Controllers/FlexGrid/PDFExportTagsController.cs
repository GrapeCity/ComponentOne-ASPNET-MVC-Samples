using MvcExplorer.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private static IEnumerable<Sale> pdfExportTagsData = Sale.GetData(500);
        public ActionResult PDFExportTags()
        {
            FullCountry taggedCountry = FullCountry.GetCountry("US");
            ViewBag.taggedCountry = taggedCountry;

            IEnumerable<Sale> taggedFlexGridData = pdfExportTagsData.Where(s => taggedCountry.Name.Equals(s.Country)).Take(10);
            ViewBag.TaggedFlexGridData = taggedFlexGridData;

            IEnumerable<Sale> taggedSumByProducts = taggedFlexGridData
                .GroupBy(s => s.Product)
                .Select(g => new Sale
                {
                    Product = g.Key,
                    Amount2 = g.Sum(s => s.Amount2)
                });
            ViewBag.TaggedSumByProducts = taggedSumByProducts;

            return View();
        }
    }
}