using System.Web.Mvc;
using FlexSheet101.Models;

namespace FlexSheet101.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            FlexSheetModel model = new FlexSheetModel();
            model.CountryData = Sale.GetData(500);
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            return View(model);
        }
    }
}