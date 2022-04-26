using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlexSheetExplorer.Models;
using C1.Web.Mvc;
using C1.Web.Mvc.Serialization;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public static List<Sale> SALES = CustomerSale.GetData(50).ToList();

        public ActionResult DataBinding()
        {
            return View(SALES);
        }
    }
}
