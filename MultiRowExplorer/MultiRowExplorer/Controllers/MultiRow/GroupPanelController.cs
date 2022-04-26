using MultiRowExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MultiRowExplorer.Controllers
{
    public partial class MultiRowController : Controller
    {
        private readonly ControlOptions _groupPanelOptions = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Max Groups", new OptionItem {Values = new List<string> {"3", "4", "5", "6"}, CurrentValue = "3"}},
                {"Placeholder", new OptionItem {Values = new List<string> {Resources.MultiRowExplorer.GroupPanel_Text1, Resources.MultiRowExplorer.GroupPanel_Text2}, CurrentValue = Resources.MultiRowExplorer.GroupPanel_Text1}}
            }
        };

        public ActionResult GroupPanel(FormCollection data)
        {
            _groupPanelOptions.LoadPostData(data);
            ViewBag.DemoOptions = _groupPanelOptions;
            return View(Orders.GetOrders());
        }
    }
}
