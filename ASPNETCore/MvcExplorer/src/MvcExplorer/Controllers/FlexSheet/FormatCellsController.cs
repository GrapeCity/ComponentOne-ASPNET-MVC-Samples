using MvcExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult FormatCells()
        {
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            ViewBag.DemoDescription = false;
            return View();
        }
    }
}
