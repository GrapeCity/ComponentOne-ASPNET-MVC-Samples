using ReportViewer101.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ReportViewer101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ReportViewer101Model model = new ReportViewer101Model();
            model.ServiceUrl = "https://developer.mescius.com/componentone/demos/aspnet/c1webapi/latest/api/report";
            model.ZoomFactor = 1;
            model.ReportNames = GetReportNames();
            model.Parameters = GetParameters();
            model.MouseModes = new List<CmbList>()
            {
                new CmbList("SelectTool", "Select"),
                new CmbList("MoveTool", "Move"),
                new CmbList("RubberbandTool", "Rubberband"),
                new CmbList("MagnifierTool", "Magnifier"),
            };
            return View(model);
        }

        private List<CmbList> GetReportNames()
        {
            List<CmbList> reportNames = new List<Models.CmbList>();
            reportNames.Add(new CmbList("ReportsRoot/Formatting/AlternateBackground.flxr/AlternateBackground", "Alternating Background"));
            reportNames.Add(new CmbList("ReportsRoot/Controls/AllCharts.flxr/AllCharts", "All Charts"));
            reportNames.Add(new CmbList("ReportsRoot/Controls/CheckBox.flxr/CheckBox", "Check Box"));
            reportNames.Add(new CmbList("ReportsRoot/Controls/Shapes.flxr/Shapes", "Shapes"));
            return reportNames;
        }

        private List<CmbList> GetParameters()
        {
            List<CmbList> parameters = new List<Models.CmbList>();
            parameters.Add(new CmbList("1", "Beverages"));
            parameters.Add(new CmbList("2", "Condiments"));
            parameters.Add(new CmbList("3", "Confections"));
            parameters.Add(new CmbList("4", "Dairy Products"));
            parameters.Add(new CmbList("5", "Grains/Cereals"));
            parameters.Add(new CmbList("6", "Meat/Poultry"));
            parameters.Add(new CmbList("7", "Produce"));
            parameters.Add(new CmbList("8", "Seafood"));
            return parameters;
        }
    }
}
