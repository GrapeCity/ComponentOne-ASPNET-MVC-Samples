using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _gridGroupPanelModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Max Groups", new OptionItem {Values = new List<string> {"3", "4", "5", "6"}, CurrentValue = "3"}},
                {"Placeholder", new OptionItem {Values = new List<string> {Resources.FlexGrid.GroupPanel_Placeholder1, Resources.FlexGrid.GroupPanel_Placeholder2}, CurrentValue = Resources.FlexGrid.GroupPanel_Placeholder1}},
                {"Hide Grouped Columns", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Group Description Creator", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}},
                {"Show Drag Glyphs", new OptionItem {Values = new List<string> {"True", "False"}, CurrentValue = "False"}}
            }
        };

        public ActionResult GroupPanel(FormCollection data)
        {
            _gridGroupPanelModel.LoadPostData(data);
            ViewBag.DemoOptions = _gridGroupPanelModel;
            return View(Sale.GetData(500));
        }
    }
}
