using FlexSheetExplorer.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexSheetExplorer.Controllers
{
    public partial class FlexSheetController : Controller
    {
        public ActionResult FormatCells()
        {
            ViewBag.FontList = FontName.GetFontNameList();
            ViewBag.FontSizeList = FontSize.GetFontSizeList();
            return View();
        }
    }
}
