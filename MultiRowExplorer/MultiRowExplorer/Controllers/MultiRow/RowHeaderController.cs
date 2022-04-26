using MultiRowExplorer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _rowHeaderOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {                                                                                                                                     
                {"Row Header",new OptionItem{Values = new List<string> {"True", "False"}, CurrentValue = "True"}}
            }
        };

        // GET: GroupHeaders
        public ActionResult RowHeader(FormCollection collection)
        {
            _rowHeaderOptions.LoadPostData(collection);
            var model = Orders.GetOrders();
            ViewBag.RowHeaderOptions = _rowHeaderOptions;
            return View(model);
        }
    }
}