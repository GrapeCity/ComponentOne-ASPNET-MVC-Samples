using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExcelImportExport.Models;
using C1.Web.Mvc.Grid;

namespace ExcelImportExport.Controllers
{
    public class HomeController : Controller
    {
        //Controller Action Method for Index.cshtml
        public ActionResult Index()
        {
            FlexGridModel model = new FlexGridModel();
            model.CountryData = Sale.GetData(500);
            model.GroupBy = new string[] { "Country", "Product", "Color" };
            return View(model);
        }
    }
}
