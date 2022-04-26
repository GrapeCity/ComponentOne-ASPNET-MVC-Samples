using System.Collections.Generic;
using System.Web.Mvc;
using MultiRowExplorer.Models;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _pagingOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Page Size", new OptionItem {Values = new List<string> {"10", "25", "50", "100"}, CurrentValue = "10"}},
            }
        };

        public ActionResult Paging(FormCollection data)
        {
            _pagingOptions.LoadPostData(data);
            ViewBag.DemoOptions = _pagingOptions;
            return View(Orders.GetOrders());
        }
    }
}
