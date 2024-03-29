﻿using FlexSheetExplorer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
