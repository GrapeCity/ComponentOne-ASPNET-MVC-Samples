using System.Collections.Generic;
using System.Web.Mvc;
using MvcExplorer.Models;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridPagingModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Page Size", new OptionItem {Values = new List<string> {"10", "25", "50", "100"}, CurrentValue = "25"}},
            }
        };

        public ActionResult Paging(FormCollection data)
        {
            _gridPagingModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridPagingModel;
            return View(Sale.GetData(500));
        }
    }
}
