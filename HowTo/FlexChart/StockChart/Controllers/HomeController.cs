using StockChart.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            StockDataModel PModel1 = new StockDataModel();
            return View(PModel1);
        }

        [HttpPost]
        public ActionResult Index(StockDataModel PModel1)
        {
            PModel1.RefershStockDataModel();

            return View(PModel1);
        }
    }
}
