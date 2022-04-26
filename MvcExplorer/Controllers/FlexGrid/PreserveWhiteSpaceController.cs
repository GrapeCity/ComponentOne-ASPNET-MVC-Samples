using MvcExplorer.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MvcExplorer.Controllers
{
    public partial class FlexGridController : Controller
    {
        private readonly ControlOptions _preserveWhiteSpaceDataModel = new ControlOptions
        {
            Options = new OptionDictionary
            {
                {"Preserve White Space", new OptionItem {Values = new List<string> { "True", "False"}, CurrentValue = "True"}}
            }
        };

        public ActionResult PreserveWhiteSpace(FormCollection collection)
        {
            IValueProvider data = collection;
            _preserveWhiteSpaceDataModel.LoadPostData(data);
            ViewBag.DemoOptions = _preserveWhiteSpaceDataModel;
            var model = Sale.GetData(50).ToList();
            model.ForEach(x => {
                if(x.Color.Equals("Black") || x.Color.Equals("White"))
                {
                    x.Color = string.Format("       {0} ", x.Color);
                }
            });
            return View(model);
        }
    }
}